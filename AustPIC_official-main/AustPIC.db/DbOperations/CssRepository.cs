using System;
using System.Collections.Generic;
using System.Linq;
using AustPIC.Models;

namespace AustPIC.db.DbOperations
{
    public class CssRepository
    {

        public List<CssVariableModel> GetAllCssVariables()
        {
            using (var context = new AustPICEntities())
            {
                var result = context.CssVariables1.Select(x => new CssVariableModel()
                {
                    VarId = x.VarId,
                    VarName = x.VarName,
                    VarValue = x.VarValue
                }).ToList();

                return result;
            }
        }

        public CssVariableModel GetCssVariables(int id)
        {
            using (var context = new AustPICEntities())
            {
                var result = context.CssVariables1
                    .Where(x => x.VarId == id)
                    .Select(x => new CssVariableModel()
                    {
                        VarId = x.VarId,
                        VarName = x.VarName,
                        VarValue = x.VarValue
                    }).FirstOrDefault();

                return result;
            }
        }

        private bool IsImageFile(string varValue)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            string extension = System.IO.Path.GetExtension(varValue);
            return imageExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }

        public bool UpdateCssVariables(int id, CssVariableModel model)
        {
            using (var context = new AustPICEntities())
            {
                var evnt = new CssVariables1();

                if (evnt != null)
                {
                    evnt.VarId = id;
                    evnt.VarName = model.VarName;

                    if (IsImageFile(model.VarValue))
                    {
                        string imgFileName = System.IO.Path.GetFileName(model.VarValue);
                        evnt.VarValue = "url(\"../img/" + imgFileName + "\")";
                    }
                    else
                    {
                        evnt.VarValue = model.VarValue;
                    }
                }

                context.Entry(evnt).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return true;

            }
        }

    }


}
