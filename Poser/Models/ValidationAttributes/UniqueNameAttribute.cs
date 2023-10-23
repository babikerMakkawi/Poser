//using System;
//using System.ComponentModel.DataAnnotations;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System.Linq;

//[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
//public class UniqueNameAttribute<T> : ValidationAttribute where T : class
//{
//    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//    {
//        var dbContext = validationContext.GetRequiredService<DbContext>(); // Inject your DbContext here

//        if (value != null)
//        {
//            var name = value.ToString();
//            var propertyName = validationContext.MemberName;
//            var tableName = dbContext.Model.FindEntityType(typeof(T)).GetTableName();
//            var existingItem = dbContext.Set<T>().FromSqlRaw($"SELECT * FROM {tableName} WHERE {propertyName} = {{0}}", name).FirstOrDefault();

//            if (existingItem != null)
//            {
//                return new ValidationResult($"The {propertyName} '{name}' is already in use.");
//            }
//        }

//        return ValidationResult.Success;
//    }
//}
