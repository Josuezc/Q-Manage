using Microsoft.AspNetCore.Mvc;

public class DebugController : Controller
{
    // ✅ Prueba simple sin archivos
    public IActionResult TestFile()
    {
        Console.WriteLine("🔍 Test funcionando correctamente.");
        return View();
    }

    // ✅ Prueba de subida de archivo sin procesarlo
    [HttpPost]
    public IActionResult TestFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("❌ No se recibió ningún archivo.");
        }

        Console.WriteLine($"📂 Archivo recibido: {file.FileName} (Tamaño: {file.Length} bytes)");

        return Ok($"✅ Archivo '{file.FileName}' recibido correctamente.");
    }
}
