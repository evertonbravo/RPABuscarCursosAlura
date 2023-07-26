using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace BuscarCurossAlura
{
    public  class Automacao
    {
        public IWebDriver driver;
        public IWebDriver driver2;
        static HttpClient client;
        static DateTime data;
        List<Curso> listaCurso;
        List<Cabecalho> listaCabecalho;
        IList<IWebElement> listaElementos;
        public Automacao()
        {
            driver = new ChromeDriver();
            client = new HttpClient();
            data = DateTime.Now;
            listaCurso = new List<Curso>();
            listaCabecalho = new List<Cabecalho>();

        }
        public void BuscarCurso(string palavraPesquisada)
        {
            listaElementos = AcessarSiteeRecuperarResultados(palavraPesquisada);
            CapturarEnderecoDecricaoETitulo(listaElementos);
            PreencherCurso(palavraPesquisada);
            SalvarCurso();
        }
        static async Task<Uri> CreateCursoAsync(Curso curso)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/Curso", curso);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        public IList<IWebElement> AcessarSiteeRecuperarResultados(string palavraPesquisada)
        {
            driver.Navigate().GoToUrl("https://www.alura.com.br");
            driver.FindElement(By.Name("query")).SendKeys(palavraPesquisada);
            driver.FindElement(By.XPath("/html/body/main/section[1]/header/div/nav/div[1]/form/button")).Click();
            return driver.FindElements(By.ClassName("busca-resultado"));

        }

        public void CapturarEnderecoDecricaoETitulo(IList<IWebElement> listaElementos)
        {
            string titulo = ""; 
            string descricao = "";
            foreach (IWebElement e in listaElementos)
            {

                var elementLink = e.FindElements(By.ClassName("busca-resultado-link"));
                string endereco = elementLink.First().GetAttribute("href").ToString();


                titulo = e.FindElement(By.ClassName("busca-resultado-nome")).Text;
                descricao = e.FindElement(By.ClassName("busca-resultado-descricao")).Text;

                Cabecalho cabecalho = new Cabecalho();
                cabecalho.Endereco = endereco;
                cabecalho.Descricao = descricao;
                cabecalho.Titulo = titulo;

                listaCabecalho.Add(cabecalho);
            }

        }

        public string CapturarCargaHoraria()
        {
            string carga = "";
            var elemento = driver.FindElements(By.ClassName("courseInfo-card-wrapper-infos"));
            if (elemento.Count > 0)
            {
                carga = elemento.First().Text;
            }
            else
            {
                elemento = driver.FindElements(By.ClassName("formacao__info-destaque"));
                if (elemento.Count > 0)
                {
                    carga = elemento.First().Text;
                }
                else
                {
                    carga = "Sem Informação";
                }
            }

            return carga;

        }

        public string CapturarProfessor()
        {
            string professor = "";
            var elemento = driver.FindElements(By.ClassName("instructor-title"));
            if (elemento.Count > 0)
            {
                professor = elemento.First().Text;
            }
            else
            {
                elemento = driver.FindElements(By.ClassName("formacao__info-destaque"));
                if (elemento.Count > 0)
                {
                    professor = elemento.First().Text;
                }
                else
                {
                    professor = "Sem Informação";
                }
            }

            return professor;

        }

        
        public void PreencherCurso(string palavraPesquisada)
        {
            string professor = ""; string carga = "";
            foreach (Cabecalho e in listaCabecalho)
            {
                driver.Navigate().GoToUrl(e.Endereco);

                carga = CapturarCargaHoraria();
                professor = CapturarProfessor();


                Curso curso = new Curso
                {
                    Titulo = e.Titulo,
                    CargaHoraria = carga,
                    Descricao = e.Descricao,
                    Professor = professor,
                    PalavraPesquisada = palavraPesquisada,
                    Data = data
                };
                listaCurso.Add(curso);

            }
        }

        public void SalvarCurso()
        {
            client.BaseAddress = new Uri("https://localhost:7279/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                foreach (Curso c in listaCurso)
                {
                    var mensagem = CreateCursoAsync(c);
                    Console.WriteLine($" {mensagem}");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
