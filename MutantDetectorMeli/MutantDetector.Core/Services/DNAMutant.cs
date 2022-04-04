using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantDetector.Core.Services
{
    public abstract class mutantADN
        {
            public abstract bool ValidacionCadena(char[,] datosmutante, int size);
            
        }


    public class HorizontalValidation : mutantADN
        {
            public override bool ValidacionCadena(char[,] datosmutante, int size)
            {
                int contador = 1;
                string auxiliar = "";

                for (int columns = 0; columns < size; columns++)
                {

                    for (int rows = 0; rows < size; rows++)
                    {

                        if (auxiliar == datosmutante[columns, rows].ToString())
                        {
                            contador++;

                            if (contador == 4)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            auxiliar = datosmutante[columns, rows].ToString();
                            contador = 1;
                        }
                    }
                }
                return false;
            }
        }

        public class VerticalValidation : mutantADN
        {
            public override bool ValidacionCadena(char[,] datosmutante, int size)
            {
                int contador = 1;
                string auxiliar = "";

                for (int rows = 0; rows < size; rows++)
                {

                    for (int columns = 0; columns < size; columns++)
                    {

                        if (auxiliar == datosmutante[columns, rows].ToString())
                        {
                            contador++;

                            if (contador == 4)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            auxiliar = datosmutante[columns, rows].ToString();
                            contador = 1;
                        }

                    }
                }
                return false;
            }
        }


        public class DiagonalValidation : mutantADN
        {
            public override bool ValidacionCadena(char[,] datosmutante, int size)
            {
                int contador = 1;
                string auxiliar = "";

                for (int rows = 0; rows < size; rows++)
                {

                    for (int columns = 0; columns < size; columns++)
                    {
                        if (columns == rows)
                        {
                            if (auxiliar == datosmutante[rows, columns].ToString())
                            {
                                contador++;

                                if (contador == 4)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                auxiliar = datosmutante[rows, columns].ToString();
                                contador = 1;
                            }
                        }
                    }
                }
                return false;
            }
        }

    


}
