<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Respuesta.aspx.cs" Inherits="Respuesta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
		<title>Veterinaria Cachorros SA</title>
		<meta name="keywords" content="">
		<meta name="description" content="">
		<meta http-equiv="X-UA-Compatible" content="IE=Edge">
		<meta name="viewport" content="width=device-width, initial-scale=1">
		
		<link rel="stylesheet" href="css/animate.min.css">
		<link rel="stylesheet" href="css/bootstrap.min.css">
		<link rel="stylesheet" href="css/font-awesome.min.css">
		<link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700' rel='stylesheet' type='text/css'>
		<link rel="stylesheet" href="css/templatemo-style.css">
		<script src="js/jquery.js"></script>
		<script src="js/bootstrap.min.js"></script>
        <script src="js/jquery.singlePageNav.min.js"></script>
		<script src="js/typed.js"></script>
		<script src="js/wow.min.js"></script>
		<script src="js/custom.js"></script>
        <script> 
            protected void OnPaging(object sender, GridViewPageEventArgs e)
            {
                GridView1.PageIndex = e.NewPageIndex;
                this.BindGrid();
            }
        </script>
</head>
<body style="height: 522px">
    <!-- start preloader -->
    <form id="form1" runat="server">
		<div class="preloader">
			<div class="sk-spinner sk-spinner-wave">
     	 		<div class="sk-rect1"></div>
       			<div class="sk-rect2"></div>
       			<div class="sk-rect3"></div>
      	 		<div class="sk-rect4"></div>
      			<div class="sk-rect5"></div>
     		    
     		    
     		    
     		</div>
    	</div>
    	<!-- end preloader -->

        <!-- start header -->
      <header style="padding: 1%;">
            <div class="container">
                <div class="row">
                    <div class="col-md-3 col-sm-4 col-xs-12">
                        <p><i class="fa fa-phone"></i><span> Telefono</span>4955-3789</p>
                    </div>
                    <div class="col-md-3 col-sm-4 col-xs-12">
                        <div style="display: flex;"><i class="fa fa-envelope-o"></i><span> Email</span><a href="#">info@cachorros.com.ar</a></div>
                    </div>
                    <div class="col-md-5 col-sm-4 col-xs-12">
                        <ul class="social-icon">
                            <li><span>Conocenos</span></li>
                            <li><a href="#" class="fa fa-facebook"></a></li>
                            <li><a href="#" class="fa fa-twitter"></a></li>
                            <li><a href="#" class="fa fa-instagram"></a></li>
                            <li><a href="#" class="fa fa-apple"></a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </header>
        <!-- end header -->

    	<!-- start navigation -->
		<nav class="navbar navbar-default templatemo-nav" style="display: inline-table;" role="navigation">
			<div class="container">
				<div class="navbar-header">
					<button class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
						<span class="icon icon-bar"></span>
						<span class="icon icon-bar"></span>
						<span class="icon icon-bar"></span>
					</button>
					<a href="#" class="navbar-brand">Cachorros</a>
				</div>
				<div class="collapse navbar-collapse">
					<ul class="nav navbar-nav navbar-right">
						<li><a href="#top" onclick="window.open('/Default.aspx#top','_self')">INICIO</a></li>
                        <li><a href="#about" onclick="window.open('/Default.aspx#about','_self')">SOBRE NOSOTROS</a></li>
						<li><a href="#team" onclick="window.open('/Default.aspx#team','_self')">EQUIPO</a></li>
						<li><a href="#service" onclick="window.open('/Default.aspx#service','_self')">SERVICIOS</a></li>
						<li><a href="#portfolio" onclick="window.open('/Default.aspx#portfolio','_self')">PRODUCTOS</a></li>
						<li><a href="#contact" onclick="window.open('/Default.aspx#contact','_self')">CONTACTO</a></li>
					</ul>
				</div>
			</div>
		</nav>
		<!-- end navigation -->

        
        <div style="padding: 1%;
">
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            Las acciones que el usuario puede realizar son:<br />
            <br />
        </div>
        <div style="margin-left: 80px">
            <asp:ListBox ID="ListBox1" runat="server" Height="105px" Width="209px" BackColor="Black"></asp:ListBox>
        </div>
        <div style="padding: 1%;
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        </div>
        <div style="margin-left: 80px">
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Mostar Bitacora" Visible="False" BackColor="#CCCCCC" ForeColor="Black" />
            <div id="popup" style="max-height:600px;overflow-y:scroll;">
             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                OnPageIndexChanging="OnPaging" PageSize="10" Font-Names="Arial" BackColor="#999999" BorderColor="Black" BorderStyle="Dashed" ForeColor="Black">
                <Columns>
                    <asp:BoundField ItemStyle-Width="150px" DataField="Fecha" HeaderText="Fecha" >
                    <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ItemStyle-Width="100px" DataField="Usuario" HeaderText="Usuario" >
                    <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField ItemStyle-Width="80px" DataField="Id_Usuario" HeaderText="Id_Usuario" >
                    <ItemStyle Width="80px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ItemStyle-Width="400px" DataField="Detalle" HeaderText="Detalle" >
                    <ItemStyle Width="400px"></ItemStyle>
                    </asp:BoundField>  
                </Columns>
                 <HeaderStyle BackColor="#666666" BorderColor="Black" />
                 <PagerSettings Mode="NextPreviousFirstLast" PreviousPageText="&lt;" FirstPageText="&lt;&lt;" />
                 <RowStyle BackColor="#CCCCCC" />
            </asp:GridView>
            </div>
            
               
        </div>
    </form>
</body>
</html>
