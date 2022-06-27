using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ResearchTHM.Core.Models;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace ResearchTHM.API
{
    public static class Helpers
    {
        public static Func<T, T> DynamicSelectGenerator<T>(string Fields = "")
        {
            string[] EntityFields;
            if (Fields == "")
                // get Properties of the T
                EntityFields = typeof(T).GetProperties().Select(propertyInfo => propertyInfo.Name).ToArray();
            else
                EntityFields = Fields.Split(',');

            // input parameter "o"
            var xParameter = Expression.Parameter(typeof(T), "o");

            // new statement "new Data()"
            var xNew = Expression.New(typeof(T));

            // create initializers
            var bindings = EntityFields.Select(o => o.Trim())
                .Select(o =>
                {

                // property "Field1"
                var mi = typeof(T).GetProperty(o);

                // original value "o.Field1"
                var xOriginal = Expression.Property(xParameter, mi);

                // set value "Field1 = o.Field1"
                return Expression.Bind(mi, xOriginal);
                }
            );

            // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, bindings);

            // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<T, T>>(xInit, xParameter);

            // compile to Func<Data, Data>
            return lambda.Compile();
        }


       //public static Func<Data, Data> CreateNewStatement(string fields)
       // {
       //     // input parameter "o"
       //     var xParameter = Expression.Parameter(typeof(Data), "o");

       //     // new statement "new Data()"
       //     var xNew = Expression.New(typeof(Data));

       //     // create initializers
       //     var bindings = fields.Split(',').Select(o => o.Trim())
       //         .Select(o => {

       //     // property "Field1"
       //     var mi = typeof(Data).GetProperty(o);

       //     // original value "o.Field1"
       //     var xOriginal = Expression.Property(xParameter, mi);

       //     // set value "Field1 = o.Field1"
       //     return Expression.Bind(mi, xOriginal);
       //         }
       //     );

       //     // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
       //     var xInit = Expression.MemberInit(xNew, bindings);

       //     // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
       //     var lambda = Expression.Lambda<Func<Data, Data>>(xInit, xParameter);

       //     // compile to Func<Data, Data>
       //     return lambda.Compile();
       // }

        //public class Data
        //{
        //    public Guid ContributorId { get; set; }
        //    public string ContributorName { get; set; }
        //    public string ContributorAlias { get; set; }
        //    public string CompanyCategory { get; set; }
        //    public Guid? IndustryId { get; set; }
        //    public string Industry { get; set; }

        //}
    }
}
