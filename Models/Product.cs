using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FieldLevel.Models
{
    public class Product
    {
        public static string FirstUniqueProduct(string[] products)
        {
            string returnProd = null;

            //BAD/SLOW code!
            //List<string> prodList = products.ToList();
            //IEnumerable<string> uniqueProds = products.ToList().Distinct();
            //foreach (string s in uniqueProds)
            //{
            //    int theCount = prodList.Where(e => e.ToString() == s).Count();
            //    if (theCount == 1)
            //    {
            //        returnProd = s;
            //        break;
            //    }
            //}

            //MUCH FASTER CODE
            products = products.OrderBy(e => e.ToString()).ToArray();
            string prevProduct = "";
            int prevProductCount = 0;
            for (int i = 0; i < products.Length; i++)
            {
                if (i == 0)
                {
                    prevProduct = products[0];
                    prevProductCount = 1;
                }
                else
                {
                    if (products[i] != prevProduct)
                    {
                        if (prevProductCount == 1)
                        {
                            returnProd = prevProduct;
                            break;
                        }
                        else
                        {
                            prevProduct = products[i];
                            prevProductCount = 1;
                        }
                    }
                    else
                        prevProductCount += 1;
                }
            }

            return returnProd;
        }
    }
}
