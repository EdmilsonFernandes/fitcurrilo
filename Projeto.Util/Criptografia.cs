using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography; //criptografia..

namespace Projeto.Util
{
    public class Criptografia
    {
        //método para encriptar um valor em MD5..
        public static string EncryptMD5(string valor)
        {
            MD5 md5 = new MD5CryptoServiceProvider(); //implementa o MD5..

            //criptografar o valor informado..
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(valor));

            //retornar o hash como string..
            return BitConverter.ToString(hash)
                    .Replace("-", string.Empty); //remover os separadores..
        }

        //método para encriptar um valor em SHA1..
        public static string EncryptSHA1(string valor)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider(); //implementa o MD5..

            //criptografar o valor informado..
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(valor));

            //retornar o hash como string..
            return BitConverter.ToString(hash)
                    .Replace("-", string.Empty); //remover os separadores..
        }
    }
}
