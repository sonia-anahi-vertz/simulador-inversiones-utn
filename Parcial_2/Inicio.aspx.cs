using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Parcial_2
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";

            lblPromedioNacion.Text = "";
            lblPromedioSantander.Text = "";
            lblPromedioMacro.Text = "";

            lblRendimientoAnualNacion.Text = "";
            lblRendimientoAnualSantander.Text = "";
            lblRendimientoAnualMacro.Text = "";

            lblRendiTrimNacion.Text = "";
            lblRendiTrimSantander.Text = "";
            lblRendiTrimMacro.Text = "";

            lblRendiMensNacion.Text = "";
            lblRendiMensSantander.Text = "";
            lblRendiMensMacro.Text = "";

            lblMejorInversion.Text = "";

            //DECLARO LAS VARIABLES PARA GUARDAR EL MAYOR RENDIMIENTO DE CADA BANCO
            decimal mayorNacion = 0;
            decimal mayorSantander = 0;
            decimal mayorMacro = 0;

            string modalidadNacion = "";
            string modalidadSantander = "";
            string modalidadMacro = "";

            if (decimal.TryParse(txtCapitalInicial.Text, out decimal capitalInicial) && capitalInicial > 0 )
            {
                lblMensaje.Text = "El dato ingresado es correcto";
            }
            else
            {
                lblMensaje.Text = "Ingresá un capital inicial numérico mayor que cero.";
                return;
            }


            if ((decimal.TryParse(txtNacion2023.Text, out decimal nacion2023) && nacion2023 > 0)&&
               (decimal.TryParse(txtNacion2024.Text, out decimal nacion2024) && nacion2024 > 0) &&
               (decimal.TryParse(txtNacion2025.Text, out decimal nacion2025) && nacion2025 > 0)  )               
            {
                ///lblMensaje.Text = "Ingreso Exitoso!";

                decimal promedioNacion = CalcularPromedio(nacion2023, nacion2024, nacion2025);

                ///Muestro el promedio 
                lblPromedioNacion.Text = promedioNacion.ToString("F2") + " %" ;

                ///GANANCIA ANUAL NACION
                decimal rendimientoAnualNacion = CalcularRendimientoAnual(capitalInicial, promedioNacion);
                lblRendimientoAnualNacion.Text = "$ " + rendimientoAnualNacion.ToString("F2");

                ///GANANCIA TRIMESTRAL NACION 
                decimal rendimientoTrimestralNacion = CalcularRendimientoTrimestral(capitalInicial, promedioNacion);
                lblRendiTrimNacion.Text = "$ " + rendimientoTrimestralNacion.ToString("F2");

                //GANANCIA MENSUAL NACION
                decimal rendimientoMensNacion = CalcularRendimientoMensual(capitalInicial, promedioNacion);
                lblRendiMensNacion.Text ="$ "+  rendimientoMensNacion.ToString("F2");

                //MAYOR RENDIMIENTO NACION
                mayorNacion = ObtenerMayorRendimiento(rendimientoAnualNacion, rendimientoTrimestralNacion, rendimientoMensNacion);
              
                if (mayorNacion == rendimientoAnualNacion)
                {
                    modalidadNacion = "Anual";
                }
                else if(mayorNacion == rendimientoTrimestralNacion)
                {
                    modalidadNacion = "Trimestral";
                }
                else
                {
                    modalidadNacion = "Mensual"; 
                }

            
            }
            else
            {
                lblMensaje.Text = "Revisá las tasas del Banco Nación: deben ser números mayores que cero ";
                return;
            }

            if ((decimal.TryParse(txtSantander2023.Text, out decimal santander2023) && santander2023 >0)&&
                (decimal.TryParse(txtSantander2024.Text, out decimal santander2024) && santander2024 > 0) &&
                (decimal.TryParse(txtSantander2025.Text, out decimal santander2025) && santander2025 > 0))
            {
                decimal promedioSantander = CalcularPromedio(santander2023, santander2024, santander2025);

                lblPromedioSantander.Text = promedioSantander.ToString("F2") + " %";

                //GANANCIA ANUAL SANTANDER 
                decimal rendimientoAnualSantander = CalcularRendimientoAnual(capitalInicial, promedioSantander);
                lblRendimientoAnualSantander.Text = "$ " + rendimientoAnualSantander.ToString("F2");

                //GANANCIA TRIMESTRAL SANTANDER 
                decimal rendimientoTrimestralSantander = CalcularRendimientoTrimestral(capitalInicial, promedioSantander);
                lblRendiTrimSantander.Text = "$ " + rendimientoTrimestralSantander.ToString("F2");

                //GANANCIA MENSUAL SANTANDER 
                decimal rendimientoMensualSantander = CalcularRendimientoMensual(capitalInicial, promedioSantander);
                lblRendiMensSantander.Text = "$ " + rendimientoMensualSantander.ToString("F2");

                //MAYOR RENDIMIENTO sANTANDER
                mayorSantander = ObtenerMayorRendimiento(rendimientoAnualSantander, rendimientoTrimestralSantander, rendimientoMensualSantander);
              
                if (mayorSantander == rendimientoAnualSantander)
                {
                    modalidadSantander = "Anual";
                }
                else if (mayorSantander == rendimientoTrimestralSantander)
                {
                    modalidadSantander = "Trimestral";
                }
                else
                {
                    modalidadSantander = "Mensual";
                }



            }
            else
            {
                lblMensaje.Text = "Revisá las tasas del Banco Santander: deben ser números mayores que cero.";
                return;
            }

            if ((decimal.TryParse(txtMacro2023.Text, out decimal macro2023) && macro2023 > 0) &&
                 (decimal.TryParse(txtMacro2024.Text, out decimal macro2024) && macro2024 > 0) &&
                 (decimal.TryParse(txtMacro2025.Text, out decimal macro2025) && macro2025 > 0)
                )
            {
                decimal promedioMacro = CalcularPromedio(macro2023, macro2024, macro2025);
                lblPromedioMacro.Text = promedioMacro.ToString("F2") + " %";

                //GANANCIA ANUAL MACRO
                decimal rendimientoAnualMacro = CalcularRendimientoAnual(capitalInicial, promedioMacro);
                lblRendimientoAnualMacro.Text = "$ " + rendimientoAnualMacro.ToString("F2");

                //GANACIA TIMESTRAL MACRO 
                decimal rendimientoTrimestrallMacro = CalcularRendimientoTrimestral(capitalInicial, promedioMacro);
                lblRendiTrimMacro.Text = "$ " + rendimientoTrimestrallMacro.ToString("F2");

                //GANANCIA MENSUAL MACRO
                decimal rendimientoMensMacro = CalcularRendimientoMensual(capitalInicial, promedioMacro);
                lblRendiMensMacro.Text ="$ " +  rendimientoMensMacro.ToString("F2");

                //MAYOR RENDIMIENTO MACRO
                mayorMacro = ObtenerMayorRendimiento(rendimientoAnualMacro, rendimientoTrimestrallMacro, rendimientoMensMacro);
         
                if (mayorMacro == rendimientoAnualMacro)
                {
                    modalidadMacro = "Anual";
                }
                else if (mayorMacro == rendimientoTrimestrallMacro)
                {
                    modalidadMacro = "Trimestral";
                }
                else
                {
                    modalidadMacro = "Mensual";
                }


            }
            else
            {
                lblMensaje.Text = "Revisá las tsas del Banco Macro: deben ser números mayores que cero ";
                return;
            }

            //COMPARO LOS MAYORES RENDIMIENTOS DE LOS 3 BANCOS 
            String mejorInversion = CalcularMejorInversion(mayorNacion,mayorSantander,mayorMacro );

            string modalidadGanadora = "";

            if (mayorNacion > mayorSantander && mayorNacion > mayorMacro)
            {
                modalidadGanadora = modalidadNacion;
            }
            else if (mayorSantander > mayorNacion && mayorSantander > mayorMacro)
            {
                modalidadGanadora = modalidadSantander;
            }
            else if (mayorMacro > mayorNacion && mayorMacro > mayorSantander)
            {
                modalidadGanadora = modalidadMacro;
            }
            else
            {
                modalidadGanadora = modalidadNacion;
            }

            ///MOSTRAR EL VALOR DE LA GANACIA MAXIMA 
            decimal gananciaMaxima = mayorNacion;

            if (mayorSantander > gananciaMaxima)
            {
                gananciaMaxima = mayorSantander;
            }

            if (mayorMacro > gananciaMaxima)
            {
                gananciaMaxima = mayorMacro;
            }

            //lblMejorInversion.Text = "Mejor inversión: " + mejorInversion + " | Modalidad: " + modalidadGanadora;

            lblMejorInversion.Text =
                            "🏆 MEJOR OPCIÓN DE INVERSIÓN" + "<br/><br/>" +
                            "Banco recomendado: " + mejorInversion + "<br/>" +
                            "Modalidad más rentable: " + modalidadGanadora + "<br/>" +
                            "Ganancia estimada: $ " + gananciaMaxima.ToString("N2");



        }



        private decimal CalcularPromedio(decimal tasa1, decimal tasa2, decimal tasa3)
        {
            decimal promedio = (tasa1 + tasa2 + tasa3) / 3;
            return promedio;

        }

        private decimal CalcularRendimientoAnual(decimal capital, decimal tasa)
        {
            decimal rendimientoAnual = (capital * tasa) / 100;
            return rendimientoAnual;

        }

        private decimal CalcularRendimientoTrimestral(decimal capital, decimal tasa) 
        {
            decimal capitalInicial = capital;
            decimal tasaTrimestral = tasa/4;

            for (int i = 0; i < 4; i++)
            {
                decimal interes = (capital * tasaTrimestral)/ 100;
                capital += interes;
            }
            
            decimal ganacia = capital - capitalInicial;

            return ganacia; 

        }

        private decimal CalcularRendimientoMensual(decimal capital, decimal tasa)
        {
            decimal capitalInicial = capital;
            decimal tasaMensual = tasa / 12;

            for (int i = 0; i < 12; i++)
            {
                decimal interes= (capital * tasaMensual)/100;
                capital += interes;
            }
            decimal ganancia = capital - capitalInicial;
            return ganancia;
        }
        private string CalcularMejorInversion(decimal rendiNacion, decimal rendiSantander, decimal rendiMacro)
        {
            decimal mayorRendimiento = rendiNacion;
            string mejorBanco = "";

            if (mayorRendimiento > rendiSantander && mayorRendimiento > rendiMacro)
            {
                mejorBanco = "Banco Nacion"; 
            }
            if (mayorRendimiento < rendiSantander && rendiSantander > rendiMacro)
            {
                mejorBanco = "Banco Santander";
            }
            if (rendiMacro > rendiNacion && rendiMacro > rendiSantander)
            {
                mejorBanco = "Banco Macro";
            }
            if (rendiNacion == rendiSantander && rendiSantander == rendiMacro)
            {
                mejorBanco = "Los 3 bancos ofrecen el mismo rendimiento";
            }
            if (rendiNacion == rendiSantander && rendiSantander > rendiMacro)
            {
                mejorBanco = "El banco Nacion y el banco Santander ofrecen el mismo rendimiento";
            }
            if (rendiNacion > rendiSantander && rendiSantander < rendiMacro && rendiNacion == rendiMacro)
            {
                mejorBanco = "El banco Nacion y el banco Macro ofrecen el mismo rendimiento";
            }
            if (rendiNacion < rendiSantander && rendiSantander == rendiMacro)
            {
                mejorBanco = "El banco Santander y el banco Macro ofrecen el mismo rendimiento";

            }
                return mejorBanco;
        }


        // RECIBO LOS 3 RENDIMIENTOS Y DEVUELVO EL MAYOR 
        private decimal ObtenerMayorRendimiento(decimal anual, decimal trimestral, decimal mensual)
        {
            decimal mayor = anual;

            if (trimestral > mayor)
            {
                mayor = trimestral;
            }

            if (mensual > mayor)
            {
                mayor = mensual;
            }

            return mayor;
        }



        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar capital inicial
            txtCapitalInicial.Text = "";

            // Limpiar tasas
            txtNacion2023.Text = "";
            txtNacion2024.Text = "";
            txtNacion2025.Text = "";

            txtSantander2023.Text = "";
            txtSantander2024.Text = "";
            txtSantander2025.Text = "";

            txtMacro2023.Text = "";
            txtMacro2024.Text = "";
            txtMacro2025.Text = "";

            // Limpiar promedios
            lblPromedioNacion.Text = "";
            lblPromedioSantander.Text = "";
            lblPromedioMacro.Text = "";

            // Limpiar rendimientos
            lblRendimientoAnualNacion.Text = "";
            lblRendimientoAnualSantander.Text = "";
            lblRendimientoAnualMacro.Text = "";

            lblRendiTrimNacion.Text = "";
            lblRendiTrimSantander.Text = "";
            lblRendiTrimMacro.Text = "";

            lblRendiMensNacion.Text = "";
            lblRendiMensSantander.Text = "";
            lblRendiMensMacro.Text = "";

            // Limpiar mensajes
            lblMensaje.Text = "";
            lblMejorInversion.Text = "";




        }

     
    }
}