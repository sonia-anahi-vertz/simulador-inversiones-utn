<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Parcial_2.Inicio" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Simulador de Inversiones Bancarias</title>
    <link rel="icon" type="image/x-icon" href="~/favicon.ico?v=2" runat="server" />
    <style type="text/css">
        * { box-sizing: border-box; }
        body { margin: 0; background: #eef4fa; color: #142b4b; font-family: 'Segoe UI', Arial, sans-serif; }
        .cabecera { background: linear-gradient(115deg,#123b67,#2478b8); color: white; padding: 30px max(22px,calc((100% - 1160px)/2)); }
        .cabecera h1 { margin: 0; font-size: clamp(23px,3vw,34px); }
        .cabecera p { margin: 8px 0 0; color: #dcecf9; }
        .contenedor { max-width: 1160px; margin: 25px auto; padding: 0 18px 35px; }
        .superior { display: grid; grid-template-columns: 310px minmax(0,1fr); gap: 20px; align-items: stretch; }
        .tarjeta { background: white; border-radius: 15px; box-shadow: 0 5px 20px rgba(24,65,109,.10); border: 1px solid #d8e5f1; overflow: hidden; }
        .tarjeta h2 { margin: 0; background: #d8ebfc; padding: 17px 20px; font-size: 19px; color: #173d6c; }
        .contenido { padding: 22px; }
        .etiqueta { display: block; font-weight: 700; margin-bottom: 10px; }
        .campo { width: 100%; padding: 12px; border: 1px solid #b9cfe2; border-radius: 8px; background: #f8fbff; font-size: 16px; color: #183a61; }
        .campo-tasa { width: 100%; max-width: 125px; padding: 9px 6px; border: 1px solid #b9cfe2; border-radius: 7px; background: #f8fbff; text-align: center; font-size: 15px; }
        .campo:focus,.campo-tasa:focus { outline: 2px solid #75b3e7; border-color: #287fc3; }
        .boton { margin-top: 22px; width: 100%; border: none; border-radius: 9px; background: #087b43; color: white; padding: 13px 16px; font-size: 17px; font-weight: 700; cursor: pointer; }
        .boton:hover { background: #066537; }
        .tabla-scroll { overflow-x: auto; }
        table { border-collapse: collapse; width: 100%; }
        .tasas th,.tasas td { padding: 11px 10px; text-align: center; border-bottom: 1px solid #e0e9f2; }
        .tasas th { background: #eaf4fd; font-size: 14px; }
        .tasas th.banco { background: #174f85; color: white; }
        .tasas th.banco.santander { background: #bb2434; }
        .tasas td:first-child { font-weight: 700; color: #25466b; background: #f7fafd; white-space: nowrap; }
        .mensaje { margin: 20px 0; padding: 14px 18px; background: #e8f4ff; border: 1px solid #bed9f0; border-radius: 10px; color: #174b77; font-weight: 600; min-height: 50px; }
        .promedios { margin-top: 20px; }
        .promedios-grid { display: grid; grid-template-columns: repeat(3,minmax(0,1fr)); gap: 12px; padding: 18px; }
        .promedio { text-align: center; border-radius: 11px; background: #e5f3ff; padding: 17px 10px; }
        .promedio.santander { background: #fff0f1; }
        .promedio strong { display: block; margin-bottom: 10px; }
        .promedio span { font-size: 23px; font-weight: 800; color: #124d82; }
        .promedio.santander span { color: #124D82; }
        .rendimientos { margin-top: 20px; }
        .rendimientos table { min-width: 650px; }
        .rendimientos th { background: #174f85; color: white; text-align: center; padding: 14px 10px; }
        .rendimientos th:first-child { text-align: left; background: #d8ebfc; color: #173d6c; }
        .rendimientos th.santander { background: #bb2434; }
        .rendimientos td { padding: 16px 12px; text-align: center; border-bottom: 1px solid #dce7f1; font-weight: 700; color: #153e6b; }
        .rendimientos td:first-child { text-align: left; color: #183756; }
        .rendimientos tr:nth-child(even) td { background: #f6faff; }
        @media (max-width: 800px) { .superior { grid-template-columns: 1fr; } .promedios-grid { grid-template-columns: 1fr; } .cabecera { padding: 26px 20px; } }
        .mejor-inversion {
            display: block;
            background-color: #E3F2FD;
            color: #17365D;
            border-left: 5px solid #2E7D32;
            border-radius: 8px;
            padding: 20px;
            margin-top: 20px;
            font-size: 18px;
            font-weight: bold;
            text-align: center;
        }    
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <header class="cabecera">
            <h1><asp:Label ID="lblTitulo" runat="server" Text="Simulador de Inversiones Bancarias"></asp:Label></h1>
            <p>Compará tasas y calculá tus ganancias de manera sencilla.</p>
        </header>
        <main class="contenedor">
            <div class="superior">
                <section class="tarjeta">
                    <h2>Datos de la inversión</h2>
                    <div class="contenido">
                        <asp:Label ID="lblCapitalInicial" runat="server" Text="Capital inicial" AssociatedControlID="txtCapitalInicial" CssClass="etiqueta"></asp:Label>
                        <asp:TextBox ID="txtCapitalInicial" runat="server" CssClass="campo">850000</asp:TextBox>
                        <asp:Button ID="btnCalcular" runat="server" Text="Calcular" CssClass="boton" OnClick="btnCalcular_Click" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="boton" OnClick="btnLimpiar_Click" />
                    </div>

                </section>
                <section class="tarjeta">
                    <h2>Tasas de interés por año y banco</h2>
                    <div class="tabla-scroll">
                        <table class="tasas">
                            <thead><tr><th>Año</th><th class="banco">Banco Nación</th><th class="banco santander">Banco Santander</th><th class="banco">Banco Macro</th></tr></thead>
                            <tbody>
                                <tr><td><asp:Label ID="lbl2023" runat="server" Text="Tasa 2023"></asp:Label></td><td><asp:TextBox ID="txtNacion2023" runat="server" CssClass="campo-tasa"></asp:TextBox></td><td><asp:TextBox ID="txtSantander2023" runat="server" CssClass="campo-tasa"></asp:TextBox></td><td><asp:TextBox ID="txtMacro2023" runat="server" CssClass="campo-tasa"></asp:TextBox></td></tr>
                                <tr><td><asp:Label ID="lbl2024" runat="server" Text="Tasa 2024"></asp:Label></td><td><asp:TextBox ID="txtNacion2024" runat="server" CssClass="campo-tasa"></asp:TextBox></td><td><asp:TextBox ID="txtSantander2024" runat="server" CssClass="campo-tasa"></asp:TextBox></td><td><asp:TextBox ID="txtMacro2024" runat="server" CssClass="campo-tasa"></asp:TextBox></td></tr>
                                <tr><td><asp:Label ID="lbl2025" runat="server" Text="Tasa 2025"></asp:Label></td><td><asp:TextBox ID="txtNacion2025" runat="server" CssClass="campo-tasa"></asp:TextBox></td><td><asp:TextBox ID="txtSantander2025" runat="server" CssClass="campo-tasa"></asp:TextBox></td><td><asp:TextBox ID="txtMacro2025" runat="server" CssClass="campo-tasa"></asp:TextBox></td></tr>
                            </tbody>
                        </table>
                    </div>
                </section>
            </div>
            <div class="mensaje"><asp:Label ID="lblMensaje" runat="server"></asp:Label></div>
            <section class="tarjeta promedios">
                <h2>Promedio de tasas (2023–2025)</h2>
                <div class="promedios-grid">
                    <div class="promedio"><strong>Banco Nación</strong><span><asp:Label ID="lblPromedioNacion" runat="server"></asp:Label></span></div>
                    <div class="promedio santander"><strong>Banco Santander</strong><span><asp:Label ID="lblPromedioSantander" runat="server"></asp:Label></span></div>
                    <div class="promedio"><strong>Banco Macro</strong><span><asp:Label ID="lblPromedioMacro" runat="server"></asp:Label></span></div>
                </div>
            </section>
            <section class="tarjeta rendimientos">
                <h2>Rendimientos de la inversión</h2>
                <div class="tabla-scroll">
                    <table>
                        <thead><tr><th>Tipo de rendimiento</th><th>Banco Nación</th><th class="santander">Banco Santander</th><th>Banco Macro</th></tr></thead>
                        <tbody>
                            <tr><td>Rendimiento anual</td><td><asp:Label ID="lblRendimientoAnualNacion" runat="server"></asp:Label></td><td><asp:Label ID="lblRendimientoAnualSantander" runat="server"></asp:Label></td><td><asp:Label ID="lblRendimientoAnualMacro" runat="server"></asp:Label></td></tr>
                            <tr><td>Rendimiento trimestral</td><td><asp:Label ID="lblRendiTrimNacion" runat="server"></asp:Label></td><td><asp:Label ID="lblRendiTrimSantander" runat="server"></asp:Label></td><td><asp:Label ID="lblRendiTrimMacro" runat="server"></asp:Label></td></tr>
                            <tr><td>Rendimiento mensual</td><td><asp:Label ID="lblRendiMensNacion" runat="server"></asp:Label></td><td><asp:Label ID="lblRendiMensSantander" runat="server"></asp:Label></td><td><asp:Label ID="lblRendiMensMacro" runat="server"></asp:Label></td></tr>
                        </tbody>
                    </table>
                </div>
            </section>
        </main>
        <asp:Label ID="lblMejorInversion" runat="server" CssClass="mejor-inversion"></asp:Label>
    </form>
</body>
</html>
