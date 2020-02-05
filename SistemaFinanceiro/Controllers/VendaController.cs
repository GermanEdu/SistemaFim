using Model.Entity;
using Model.Neg;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SistemaFinanceiro.Controllers
{
    public class VendaController : Controller
    {
        private VendaNeg objVendaNeg;
        private ClienteNeg objClienteNeg;
        private ProdutoNeg objProdutoNeg;
        private ModoPagoNeg objModoPagoNeg;
       
        public VendaController()
        {
            objVendaNeg = new VendaNeg();
            objClienteNeg = new ClienteNeg();
            objProdutoNeg = new ProdutoNeg();
            objModoPagoNeg = new ModoPagoNeg();
          
        }
        [HttpGet]
        public ActionResult ObterClientes()
        {
            List<Cliente> lista = objClienteNeg.findAll();
            return View(lista);
        }

        [HttpPost]//para buscar clientes
        public ActionResult ObterClientes(string txtnome, string txtcpf, long txtcliente = -1)
        {
            if (txtnome == "")
            {
                txtnome = "-1";
            }
            
            if (txtcpf == "")
            {
                txtcpf = "-1";
            }
            Cliente objCliente = new Cliente();
            objCliente.Nome = txtnome;
            objCliente.IdCliente = txtcliente;
            objCliente.Cpf = txtcpf;

            List<Cliente> cliente = objClienteNeg.findAllClientes(objCliente);
            return View(cliente);
        }
       
        [HttpPost]
        public ActionResult Selecionar(string idProduto)
        {
            Produto objProduto = new Produto(idProduto);
            objProdutoNeg.find(objProduto);            
            return Json(objProduto, JsonRequestBehavior.AllowGet);
            
        }     
        

        public void carregarProdutocmb()
        {
            List<Produto> data = objProdutoNeg.findAll();
            SelectList lista = new SelectList(data, "idProduto", "nome");
            ViewBag.ListaProduto = lista;
        }
        public void carregarModoPagocmb()
        {
            List<ModoPago> data = objModoPagoNeg.findAll();
            SelectList lista = new SelectList(data, "numPag", "nome");
            ViewBag.ListaModoPago = lista;
        }

        public ActionResult NovaVenda()
        {
            carregarModoPagocmb();
            carregarProdutocmb();
            return View();
        }
        
      

    }
}