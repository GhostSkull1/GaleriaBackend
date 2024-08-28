using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImagenGa.Custom;
using ImagenGa.Models;
using ImagenGa.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.IO.Compression;

namespace ImagenGa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenController : ControllerBase
    {
        private readonly DbpruebaContext _dbPruebaContext;
        public ImagenController(DbpruebaContext dbPruebaContext)
        {
            _dbPruebaContext = dbPruebaContext;
        }

        [HttpGet]
        [Authorize]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {

            var lista = await _dbPruebaContext.Imagenes.Where(f => f.FechaE == null).ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });

        }


        [HttpPost]
        [Authorize]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar(IFormFile imagen, int  IdUsuario, string Descripcion)
        {

            try
            {
                if (!Tipoimagen(imagen.FileName))
                {
                    throw new Exception(" el archivo no es una imagen");
                }
                Imagen _imagen = new Imagen();
                _imagen.Nombre = imagen.FileName;
                _imagen.FechaC = DateTime.Now;
                _imagen.ImagenUrl = "Galeria\\" + imagen.FileName;
                _imagen.Descripcion = Descripcion;
                _imagen.NumeroDescargas = 0;

                await _dbPruebaContext.AddAsync(_imagen);
                await _dbPruebaContext.SaveChangesAsync();

                Registro registro = new Registro();
                registro.IdUsuario = IdUsuario;
                registro.IdImagen = _imagen.IdImagen;
                registro.Fecha = DateTime.Now;
                registro.IdAccion = "1";
                await _dbPruebaContext.AddAsync(registro);
                await _dbPruebaContext.SaveChangesAsync();

                using (var stream = System.IO.File.Create(_imagen.ImagenUrl)) 
                {
                    imagen.CopyTo(stream);
                }

                return Ok(new {mensaje= "Se guardo la imagen"});
            }
            catch (Exception x)
            {

                return BadRequest ("No se guardo: " + x.Message);
            }

        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        [Route("Eliminar")]
        public async Task<IActionResult> Eliminar (int idUsuario, int idimagen)
        {
            try
            {
                Imagen imagen = await _dbPruebaContext.Imagenes.FirstOrDefaultAsync(I=> I.IdImagen == idimagen);
                System.IO.File.Delete(imagen.ImagenUrl);
                Registro registro = new Registro();
                registro.IdUsuario = idUsuario;
                registro.IdImagen = imagen.IdImagen;
                registro.Fecha = DateTime.Now;
                registro.IdAccion = "2";
                await _dbPruebaContext.Registros.AddAsync(registro);
                await _dbPruebaContext.SaveChangesAsync();
                imagen.FechaE = DateTime.Now;
                _dbPruebaContext.Imagenes.Update(imagen);
                await _dbPruebaContext.SaveChangesAsync();


                return Ok(new {mensaje = "Se elimino la imagen"});
            }
            catch (Exception x)
            {

                return BadRequest ("No se elimino: " + x.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("Descargas")]
        public async Task<IActionResult> Descargas (int id)
        {
            try
            {
                var imagen = await _dbPruebaContext.Imagenes.FirstOrDefaultAsync(a => a.IdImagen == id);

                if (imagen == null)
                {
                    throw new Exception();
                }

                imagen.NumeroDescargas +=1;
                _dbPruebaContext.Imagenes.Update(imagen);
                _dbPruebaContext.SaveChanges();
                var ImagenByte = System.IO.File.ReadAllBytes(imagen.ImagenUrl);
                return File(ImagenByte, "application/octet-stream", imagen.Nombre);
            }
            catch (Exception x)
            {

                return BadRequest("No se descargo: " + x.Message);
            }
        }

        private bool Tipoimagen(string ruta)
        {
            bool Tipoimagen = false;
            var arreglo = ruta.Split('.');
            string extension = arreglo[arreglo.Length - 1].ToLower();
            if (extension == "jpg" ||
                extension == "png" ||
                extension == "jpeg" ||
                extension == "gif" ||
                extension == "bmp" ||
                extension == "tif" ||
                extension == "tiff"
                )
            {
                Tipoimagen = true;
            }
            return Tipoimagen;
        }

        [HttpGet]
        [Authorize]
        [Route("Comprimido")]
        public async Task<IActionResult> ConsultarTodosZip()
        {
            try
            {
                
                var lista = await _dbPruebaContext.Imagenes.Where(A => A.FechaE == null).ToListAsync();
                var archivosComprimidos = await ComprimirArchivos(lista, "Galeria\\archivoComprimido.zip");

               
                return File(archivosComprimidos,"Application/zip","imagenes.zip");
            }
            catch (Exception ex)
            {
               
                throw new Exception(ex.Message);
            }
        }

        private async Task<byte[]> ComprimirArchivos(List<Imagen> imagens, string _ruta)
        {
            try
            {
                
                string ruta = _ruta;
                using (var zipToCreate = new FileStream(ruta, System.IO.FileMode.Create))
                using (var archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
                {

                    foreach (var imagen in imagens)
                    {
                        var filePath = imagen.ImagenUrl;
                        if (System.IO.File.Exists(filePath))
                        {
                            var zipEntry = archive.CreateEntry(imagen.ImagenUrl);
                            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                            using (var entryStream = zipEntry.Open())
                            {
                                await fileStream.CopyToAsync(entryStream);
                            }
                        }
                    }
                }

                var archivoComprimido = await System.IO.File.ReadAllBytesAsync(ruta);
                System.IO.File.Delete(ruta);
               
                return archivoComprimido;

            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
    }
}
