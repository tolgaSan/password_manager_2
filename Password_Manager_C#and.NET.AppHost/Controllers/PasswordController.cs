using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
namespace Password_Manager.Controllers
{
    internal class PasswordController: ControllerBase
    {
        private readonly PasswordList passwordList = new PasswordList();

        [HttpGet]
        public ActionResult<List<PasswordItem>> GetAll() => passwordList.GetAll();

        [HttpGet("{id}")]
        public ActionResult<PasswordItem> GetById(int id)
        {
            var item = passwordList.GetById(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public IActionResult Create(PasswordItem passwordItem)
        {
            passwordList.Add(passwordItem);
            return CreatedAtAction(nameof(GetById), new { id = passwordItem.Id }, passwordItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PasswordItem updateItem)
        {
            var existingItem = passwordList.GetById(id);
            if (existingItem == null) return NotFound();

            existingItem.Category = updateItem.Category;
            existingItem.App = updateItem.App;
            existingItem.UserName = updateItem.UserName;
            existingItem.EncryptedPassword = updateItem.EncryptedPassword;

            passwordList.Update(existingItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingItem = passwordList.GetById(id);
            if (existingItem == null) return NotFound();

            passwordList.Delete(id);
            return NoContent();
        }

        string EncryptPassword(string plainText) => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));

        string DecryptPassword(string encrypted) => System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encrypted));


    }
}
