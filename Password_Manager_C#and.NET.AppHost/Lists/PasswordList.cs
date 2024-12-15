using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

using System.Runtime;
using Microsoft.AspNetCore.Identity;

public class PasswordList
{
    private List<PasswordItem> passwordItems;

    public List<PasswordItem> GetAll()
    {
        return passwordItems;
    }

    public PasswordItem GetById(int id)
    {
        return passwordItems.FirstOrDefault(p => p.Id == id);
    }

    public void Add(PasswordItem passwordItem)
    {
        int newId = passwordItems.Count > 0 ? passwordItems.Max(p => p.Id) + 1 : 1;
        passwordItem.Id = newId;
        passwordItems.Add(passwordItem);
    }

    public bool Update(PasswordItem passwordItem)
    {
        var toUpdateItem = GetById(passwordItem.Id);
        if (toUpdateItem == null) return false;

        toUpdateItem.Category = passwordItem.Category;
        toUpdateItem.App = passwordItem.App;
        toUpdateItem.UserName = passwordItem.UserName;
        toUpdateItem.EncryptedPassword = passwordItem.EncryptedPassword;
        return false;
    }

    public bool Delete(int id)
    {
        var toDeletePassword = GetById(id);
        if(toDeletePassword == null) return false;
        passwordItems.Remove(toDeletePassword);
        return true;
    }

}