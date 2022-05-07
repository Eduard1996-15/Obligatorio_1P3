using System;
using System.Collections.Generic;
using System.Text;
using Negocio.InterfacesRepositorios;
using System.Text.RegularExpressions;

namespace Dominio.EntidadesNegocios
{
    public class Usuario:IValidar
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool Validar()
        {
            if (ValidarMailUsuario(Email) && ValidarPassword(Password))
            {
                return true;
            }
            return false;
        }
        private bool ValidarMailUsuario(string email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";//expresion 
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private bool ValidarPassword(string pass)
        {
            int contNum = 0;
            if (pass != null && pass.Length >= 6)
            { 
                foreach (char item in pass)
                {
                    if (Char.IsNumber(item))//hay  numero
                    {
                        contNum++;
                    }
                    else if (Char.IsLetter(item))//hay letra
                    {
                        if (item.ToString().ToUpper() == item.ToString())//hay una mayuscula
                        {
                            contNum++;
                        }
                        else if (item.ToString().ToLower() == item.ToString())//hay una minuscula
                        {
                            contNum++;
                        }
                    }
                }
                if (contNum >= 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
