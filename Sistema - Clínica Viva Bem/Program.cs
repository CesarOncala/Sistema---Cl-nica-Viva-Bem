
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Sistema___Clínica_Viva_Bem
{

        class Program
        {
            public static string GerarCodigo() // Função sem parâmetro
            {
                string codigo = "";
                codigo = Guid.NewGuid().ToString().Substring(0,7).ToUpper();


                return codigo;
            }
            public static void Cadastro_Paciente() // Procedimento sem parâmetro
            {
                string codg = "", linha;
                codg = GerarCodigo();
                int teste = 0;
                string nome, endereço, tel, nasc;
                Console.WriteLine("\nPor favor informe seu nome completo");
                nome = Console.ReadLine();

                FileStream arq1 = new FileStream("Cadastro_Paciente.txt", FileMode.OpenOrCreate);
                StreamReader ler = new StreamReader(arq1);
                string[] resultado;

                do
                {
                    linha = ler.ReadLine();

                    if (linha != null)
                    {
                        resultado = linha.Split(';');

                        if (codg == resultado[0] || nome == resultado[1])
                        {

                            teste++;

                        }

                    }

                } while (linha != null);
                ler.Close();


                if (teste == 0)
                {
                    FileStream arq = new FileStream("Cadastro_Paciente.txt", FileMode.Append);
                    StreamWriter escreve = new StreamWriter(arq);
                    Console.WriteLine("\nSeu código é: " + codg);

                    Console.WriteLine("\nPor favor informe seu endereço");
                    endereço = Console.ReadLine();
                    Console.WriteLine("\nPor favor informe seu telefone");
                    tel = Console.ReadLine();
                    Console.WriteLine("\nPor favor informe sua data de nascimento (use o padrão dia/mês/ano)");
                    nasc = Console.ReadLine();
                    escreve.WriteLine(codg + ";" + nome + ";" + endereço + ";" + tel + ";" + nasc);
                    escreve.Close();

                }
                else
                {
                    Console.WriteLine("\nO código gerado ou usuário já existe.");
                    Console.ReadKey();
                }



            }
            public static void Cadastro_Consulta() // Procedimento sem parâmetro
            {
                if (File.Exists(@"Cadastro_Paciente.txt") && File.Exists(@"Cadastro_Médico.txt"))
                {

                    FileStream arq = new FileStream("Cadastro_Paciente.txt", FileMode.Open);
                    StreamReader ler = new StreamReader(arq);
                    FileStream arq1 = new FileStream("Cadastro_Médico.txt", FileMode.Open);
                    StreamReader ler1 = new StreamReader(arq1);
                    string[] resultado;
                    string[] resultado1;
                    string linha, nome, med, linha1, linha5;
                    int i = 0, i2 = 0, i3 = 0, o = 0;
                    Console.WriteLine("\nDigite o nome do paciente");
                    nome = Console.ReadLine();
                    Console.WriteLine("\nDigite o nome do médico");
                    med = Console.ReadLine();

                    do
                    {
                        linha = ler.ReadLine();
                        linha1 = ler1.ReadLine();

                        if (linha != null)
                        {
                            resultado = linha.Split(';');

                            if (nome == resultado[1])
                            {

                                Console.WriteLine("\n--------------------------------------\nO código do paciente é: " + resultado[0] + "\n--------------------------------------");
                                i2++;
                            }

                        }
                        if (linha1 != null)
                        {
                            resultado1 = linha1.Split(';');

                            if (med == resultado1[1])
                            {

                                Console.WriteLine("\n--------------------------------------\nO código do médico é: " + resultado1[0] + "\n--------------------------------------");
                                i++;
                            }

                        }



                    } while (linha != null || linha1 != null);
                    ler.Close();
                    ler1.Close();

                    if (i == 1 && i2 == 1)
                    {
                        string data = "", paciente = "", hora = "";
                        string codgMed = "", consulta = "";

                        consulta = GerarCodigo();
                        Console.WriteLine("\n--------------------------------------\nO código da consulta: " + consulta + "\n--------------------------------------");


                        Console.WriteLine("\nInforme a data da consulta (use o padrão dia/mês/ano) ");
                        data = Console.ReadLine();
                        Console.WriteLine("\nInforme a hora da consulta (use o padrão hora:minutos) ");
                        hora = Console.ReadLine();

                        FileStream arq5 = new FileStream("Cadastro_Consulta.txt", FileMode.OpenOrCreate);
                        StreamReader ler5 = new StreamReader(arq5);
                        string[] resultado5;
                        do
                        {
                            linha5 = ler5.ReadLine();
                            if (linha5 != null)
                            {
                                resultado5 = linha5.Split(';');
                                if (resultado5[1] == data)
                                {


                                    DateTime datab = Convert.ToDateTime(resultado5[2]);
                                    DateTime datau = Convert.ToDateTime(hora);
                                    TimeSpan dif = new TimeSpan(0);

                                    if (datau.Minute < datab.Minute)
                                    {
                                        dif = datab - datau;
                                    }
                                    else if (datau.Minute > datab.Minute)
                                    {
                                        dif = datau - datab;
                                    }



                                    if (datab.Hour == datau.Hour)
                                    {
                                        Console.WriteLine("\nhorarios ocupado: " + datab.Hour + ":" + datab.Minute);

                                        if (dif.Minutes >= 30 && datab.Minute != datau.Minute)
                                        {

                                        }

                                        else
                                        {

                                            o++;

                                        }

                                    }



                                }
                            }

                        } while (linha5 != null);
                        ler5.Close();



                        if (o >= 1)
                        {

                            Console.WriteLine("\nHora não aceita, a hora digitada pelo usuário ja está cadastrada \nou não atende ao intervalo de 30 minutos entre as consultas já marcadas");
                            Console.ReadKey();
                        }


                        if (o < 1)
                        {

                            Console.WriteLine("\nInforme o código do médico ");
                            codgMed = Console.ReadLine();
                            Console.WriteLine("\nInforme o código do paciente ");
                            paciente = Console.ReadLine();

                            FileStream arq3 = new FileStream("Cadastro_Consulta.txt", FileMode.OpenOrCreate);
                            StreamReader ler3 = new StreamReader(arq3);
                            string[] resultado3;
                            string linha3;

                            do
                            {
                                linha3 = ler3.ReadLine();
                                if (linha3 != null)
                                {
                                    resultado3 = linha3.Split(';');

                                    if (resultado3[1] == data && resultado3[6] == med)
                                    {
                                        i3++;
                                    }

                                }

                            } while (linha3 != null);

                            ler3.Close();
                        }



                        if (i3 < 2)
                        {
                            if (o < 1)
                            {
                                FileStream arq2 = new FileStream("Cadastro_Consulta.txt", FileMode.Append);
                                StreamWriter escreve = new StreamWriter(arq2);
                                escreve.WriteLine(consulta + ";" + data + ";" + hora + ";" + codgMed + ";" + paciente + ";" + nome + ";" + med);

                                escreve.Close();
                            }

                        }
                        else
                        {
                            Console.WriteLine("\nNão foi possível concluir o cadrasto da consulta, pois este médico já possui dois pacientes marcados para esse dia");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        if (i2 == 1)
                        {
                            Console.WriteLine("\nEsse médico não foi cadastrado no sistema, por favor, faça o seu cadastro, e tente novamente.");
                            Console.ReadKey();
                        }
                        else if (i == 1)
                        {
                            Console.WriteLine("\nEsse paciente não foi cadastrado no sistema, por favor, faça o seu cadastro, e tente novamente.");
                            Console.ReadKey();
                        }
                        else
                        {

                            Console.WriteLine("\nEsse paciente e esse médico, não foram cadastrados no sistema, por favor faça os respectivos cadastros, e tente novamente.");
                            Console.ReadKey();


                        }


                    }




                }

                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\nPor favor cadastre pelo menos um paciente e um médico para continuar");
                    Console.ReadKey();
                }



            }
            public static void Cadastro_Médico() // Procedimento sem parâmetro
            {
                int teste = 0;
                string linha, codg, nome;
                codg = GerarCodigo();

                Console.WriteLine("\nPor favor informe seu nome completo");
                nome = Console.ReadLine();
                FileStream arq1 = new FileStream("Cadastro_Médico.txt", FileMode.OpenOrCreate);
                StreamReader ler = new StreamReader(arq1);
                string[] resultado;

                do
                {
                    linha = ler.ReadLine();

                    if (linha != null)
                    {
                        resultado = linha.Split(';');

                        if (codg == resultado[0] || nome == resultado[1])
                        {

                            teste++;

                        }

                    }

                } while (linha != null);
                ler.Close();

                if (teste == 0)
                {
                    string telefone, especialidade = "";
                    int resp = 0;
                    FileStream arq = new FileStream("Cadastro_Médico.txt", FileMode.Append);
                    StreamWriter escreve = new StreamWriter(arq);
                    string codigomed = "";
                    codigomed = GerarCodigo();
                    Console.WriteLine("\nSeu código é: " + codigomed);

                    Console.WriteLine("\nPor favor, informe seu telefone");
                    telefone = Console.ReadLine();

                    do
                    {
                        Console.Clear();
                        Console.WriteLine("___________________________________________________________________________________________________________");
                        Console.WriteLine("\nQual sua especialidade?\n \n1-Cardiologia \n2-Dermatologia \n3-Clínica médica \n___________________________________________________________________________________________________________ ");
                        resp = int.Parse(Console.ReadLine());

                        if (resp == 1)
                        {
                            especialidade = "Cardiologia";
                        }
                        if (resp == 2)
                        {
                            especialidade = "Dermatologia";
                        }
                        if (resp == 3)
                        {
                            especialidade = "Clínica médica";
                        }

                    } while (resp != 1 && resp != 2 && resp != 3);


                    escreve.WriteLine(codigomed + ";" + nome + ";" + telefone + ";" + especialidade);
                    escreve.Close();
                }
                else
                {
                    Console.WriteLine("\nO código gerado ou usuário já existe.");
                    Console.ReadKey();
                }


            }

            public static void Relatorio() // Procedimento sem parâmetro
            {
                int op, i = 0, o = 0;
                string data, linha, nome;
                if (File.Exists(@"Cadastro_Consulta.txt"))
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("___________________________________________________________________________________________________________");
                        Console.WriteLine("\nQual relatório deseja ver?\n \n1-Consultas de uma determinada data \n2-Histórico de consultas cadastradas do paciente até a data corrente \n3-Voltar ao menu principal\n___________________________________________________________________________________________________________");

                        op = int.Parse(Console.ReadLine());
                        if (op == 1)
                        {

                            Console.WriteLine("\nDigite uma data para conferir todas as consultas marcadas nesse dia (use o padrão dia/mês/ano)");
                            data = Console.ReadLine();
                            FileStream arq = new FileStream("Cadastro_Consulta.txt", FileMode.Open);
                            StreamReader ler = new StreamReader(arq);
                            string[] resultado;

                            do
                            {
                                linha = ler.ReadLine();
                                if (linha != null)
                                {
                                    resultado = linha.Split(';');
                                    if (data == resultado[1])
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("\nCódigo da consulta: " + resultado[0] + ",  marcada nesta data: " + resultado[1] + ", as: " + resultado[2] + " horas " + ", com o médico: " + resultado[6] + ", \npaciente: " + resultado[5] + ", código do médico: " + resultado[3] + ", código do paciente: " + resultado[4]);
                                        i++;

                                    }


                                }

                            } while (linha != null);
                            ler.Close();
                       
                            if (i < 1)
                            {
                                Console.WriteLine("\nNão existe consulta marcada nessa data.");
                                Console.ReadKey();
                            }



                        }
                        if (op == 2)
                        {
                            Console.WriteLine("\nDigite o seu nome para ver o seu histórico de consultas cadastradas");
                            nome = Console.ReadLine();
                            FileStream arq1 = new FileStream("Cadastro_Consulta.txt", FileMode.OpenOrCreate);
                            StreamReader ler1 = new StreamReader(arq1);
                            string[] resultado1;
                            Console.WriteLine("\nConsultas já realizadas do(a) paciente " + nome + ":");
                            do
                            {
                                linha = ler1.ReadLine();
                                if (linha != null)
                                {
                                    resultado1 = linha.Split(';');
                                    if (nome == resultado1[5])
                                    {

                                        o++;
                                        Console.WriteLine("\nCódigo da consulta: " + resultado1[0] + ",  marcada nesta data: " + resultado1[1] + ", as: " + resultado1[2] + " horas " + ", com o médico: " + resultado1[6] + ", \npaciente: " + resultado1[5] + ", código do médico: " + resultado1[3] + ", código do paciente: " + resultado1[4]);
                                    }

                                }

                            } while (linha != null);
                            ler1.Close();
                            if (o < 1)
                            {

                                Console.WriteLine("\nO(a) paciente " + nome + ", não teve nenhuma consulta realizada até o momento");

                            }
                            Console.ReadKey();
                        }
                    } while (op != 3);

                }
                else
                {
                    Console.WriteLine("\nNão existe nenhum cadastro de consulta");
                    Console.ReadKey();
                }



            }
            public static void Cancelar_consulta()
            {
                if (File.Exists(@"Cadastro_Consulta.txt"))
                {
                    string linha, cdc = "", data = "";
                    int o = 0;
                    Console.WriteLine("\nDigite a data da consulta a ser cancelada (use o padrão dia/mês/ano)");
                    data = Console.ReadLine();
                    FileStream arq1 = new FileStream("Cadastro_Consulta.txt", FileMode.Open);
                    StreamReader ler1 = new StreamReader(arq1);
                    string[] resultado1;

                    do
                    {
                        linha = ler1.ReadLine();
                        if (linha != null)
                        {
                            resultado1 = linha.Split(';');
                            if (data == resultado1[1])
                            {
                                Console.WriteLine();
                                Console.WriteLine("\nCódigo da consulta: " + resultado1[0] + ",  marcada nesta data: " + resultado1[1] + ", as: " + resultado1[2] + " horas " + ", com o médico: " + resultado1[6] + ", \npaciente: " + resultado1[5] + ", código do médico: " + resultado1[3] + ", código do paciente: " + resultado1[4]);
                                o++;
                            }


                        }

                    } while (linha != null);
                    ler1.Close();
                    if (o < 1)
                    {
                        Console.WriteLine("\nNão existem consultas marcadas nesta data");
                        Console.ReadKey();
                    }
                    if (o >= 1)
                    {
                        FileStream arq = new FileStream("Cadastro_Consulta.txt", FileMode.Open);
                        StreamReader ler = new StreamReader(arq);
                        string[] resultado;
                        FileStream aux = new FileStream("auxiliar.txt", FileMode.Create);
                        StreamWriter escreve = new StreamWriter(aux);

                        Console.WriteLine("\nPor favor digite o código da consulta que deseja cancelar");
                        cdc = Console.ReadLine();

                        do
                        {
                            linha = ler.ReadLine();
                            if (linha != null)
                            {
                                resultado = linha.Split(';');

                                if (!(cdc == resultado[0]))
                                {

                                    escreve.WriteLine(linha);

                                }
                                else
                                {

                                    Console.WriteLine("\nConsulta cancelada");
                                    Console.ReadKey();
                                }
                            }

                        } while (linha != null);
                        ler.Close();
                        escreve.Close();

                        File.Move(@"Cadastro_Consulta.txt", @"Cadastro_Consulta_old.txt");

                        File.Move(@"auxiliar.txt", @"Cadastro_Consulta.txt");

                        File.Delete(@"Cadastro_Consulta_old.txt");

                    }


                }
                else
                {

                    Console.WriteLine("\nNão existe nenhum cadastro de consulta");
                    Console.ReadKey();
                }


            }
            public static void Consulta_Médico()
            {
                if (File.Exists(@"Cadastro_Consulta.txt") && File.Exists(@"Cadastro_Médico.txt"))
                {
                    string nome, linha;
                    Console.WriteLine("\nDigite o nome do médico");
                    nome = Console.ReadLine();
                    FileStream arq1 = new FileStream("Cadastro_Consulta.txt", FileMode.Open);
                    StreamReader ler = new StreamReader(arq1);
                    string[] resultado;
                    Console.WriteLine("\nConsultas do(a) Médico(a): " + nome + "\n");
                    int o = 0;
                    do
                    {


                        linha = ler.ReadLine();
                        if (linha != null)
                        {
                            resultado = linha.Split(';');

                            if (nome == resultado[6])
                            {
                                Console.WriteLine("Código da consulta: " + resultado[0] + " ,Paciente: " + resultado[5] + " ,no dia: " + resultado[1] + " ,as: " + resultado[2] + " horas" + "\ncódigo do médico: " + resultado[3] + ", código do paciente: " + resultado[4]);
                                o++;
                            }


                        }

                    } while (linha != null);
                    if (o < 1)
                    {
                        Console.WriteLine("Este médico não está cadastrado");
                    }
                    Console.ReadKey();
                    ler.Close();
                }
                else
                {
                    Console.WriteLine("\nPor favor, para continuar, cadastre pelo menos um médico e uma consulta ");
                    Console.ReadKey();
                }
            }
            public static void info()
            {
                if (File.Exists(@"Cadastro_Médico.txt") && File.Exists(@"Cadastro_Paciente.txt"))
                {
                    int op = 0;
                    string med, linha;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("___________________________________________________________________________________________________________");
                        Console.WriteLine("\nPor favor escolha uma das opções: \n \n1-Informações do Médico \n2-Informações do paciente \n3-Procurar médico por especialidade \n4-Sair\n___________________________________________________________________________________________________________");
                        op = int.Parse(Console.ReadLine());

                        if (op == 1)
                        {
                            int o = 0;
                            Console.WriteLine("\nDigite o nome do médico(a)");
                            med = Console.ReadLine();
                            FileStream arq = new FileStream("Cadastro_Médico.txt", FileMode.Open);
                            StreamReader ler = new StreamReader(arq);
                            string[] resultado;
                            Console.WriteLine("\nInformações do médico: " + med);
                            do
                            {
                                linha = ler.ReadLine();
                                if (linha != null)
                                {
                                    resultado = linha.Split(';');
                                    if (resultado[1] == med)
                                    {
                                        Console.WriteLine("\nCódigo do médico: " + resultado[0] + ", telefone: " + resultado[2] + " ,especialidade: " + resultado[3]);
                                        o++;
                                        Console.ReadKey();
                                    }

                                }


                            } while (linha != null);
                            if (o < 1)
                            {

                                Console.WriteLine("\nEste médico não esta cadastrado no sistema");
                                Console.ReadKey();

                            }
                            ler.Close();
                        }
                        if (op == 2)
                        {
                            int o = 0;
                            Console.WriteLine("\nDigite o nome do(a) paciente");
                            med = Console.ReadLine();
                            FileStream arq = new FileStream("Cadastro_Paciente.txt", FileMode.Open);
                            StreamReader ler = new StreamReader(arq);
                            string[] resultado;
                            Console.WriteLine("\nInformações do paciente: " + med);
                            do
                            {
                                linha = ler.ReadLine();
                                if (linha != null)
                                {
                                    resultado = linha.Split(';');
                                    if (resultado[1] == med)
                                    {
                                        Console.WriteLine("\nCódigo do paciente: " + resultado[0] + ", endereço: " + resultado[2] + ", telefone: " + resultado[3] + " , data de nascimento: " + resultado[4]);
                                        o++;
                                        Console.ReadKey();
                                    }

                                }


                            } while (linha != null);
                            if (o < 1)
                            {

                                Console.WriteLine("\nEste paciente não esta cadastrado no sistema");
                                Console.ReadKey();

                            }
                            ler.Close();
                        }
                        if (op == 3)
                        {
                            int resp = 0, o = 0;
                            string especialidade = "";
                            do
                            {

                                Console.Clear();
                                Console.WriteLine("___________________________________________________________________________________________________________");
                                Console.WriteLine("\nVocê precisa de um médico especialista em que ? \n \n1-Cardiologia \n2-Dermatologia \n3-Clínica médica \n___________________________________________________________________________________________________________");
                                resp = int.Parse(Console.ReadLine());

                                if (resp == 1)
                                {
                                    especialidade = "Cardiologia";
                                }
                                if (resp == 2)
                                {
                                    especialidade = "Dermatologia";
                                }
                                if (resp == 3)
                                {
                                    especialidade = "Clínica médica";
                                }

                            } while (resp != 1 && resp != 2 && resp != 3);


                            FileStream arq = new FileStream("Cadastro_Médico.txt", FileMode.Open);
                            StreamReader ler = new StreamReader(arq);
                            string[] resultado;

                            Console.WriteLine("\nOs seguintes médicos são especialistas em: " + especialidade);
                            do
                            {
                                linha = ler.ReadLine();
                                if (linha != null)
                                {
                                    resultado = linha.Split(';');
                                    if (resultado[3] == especialidade)
                                    {
                                        Console.WriteLine("\nMédico: " + resultado[1] + ", telefone: " + resultado[2] + " , Código do médico: " + resultado[0]);

                                        o++;

                                    }

                                }


                            } while (linha != null);
                            Console.ReadKey();
                            if (o < 1)
                            {

                                Console.WriteLine("\nNão existem médicos cadastrados com esta especialidade");
                                Console.ReadKey();

                            }
                            ler.Close();
                        }

                    } while (op != 4);
                }
                else
                {
                    Console.WriteLine("\nPor favor, para continuar, cadastre pelo menos um médico e um paciente para continuar ");
                    Console.ReadKey();
                }
            }
            static void Main(string[] args)
            {
             int op = 0;

             do
             {
                 Console.Clear();
                 Console.WriteLine("___________________________________________________________________________________________________________");

                 Console.WriteLine();
                 Console.WriteLine("                                         ____CLÍNICA VIVA BEM____\n                                                ");
                 Console.WriteLine("\nEscolha uma das opções :\n \n1-Cadastrar paciente \n2-Cadastrar médico \n3-Cadastrar consulta \n4-Cancelar consulta \n5-Relatórios  \n6-Consultas do(a) médico(a) \n7-Informações úteis \n8-Sair \n___________________________________________________________________________________________________________");
                 op = int.Parse(Console.ReadLine());

                 if (op == 1)
                 {
                     Cadastro_Paciente();
                 }
                 if (op == 2)
                 {
                     Cadastro_Médico();
                 }
                 if (op == 3)
                 {
                     Cadastro_Consulta();
                 }

                 if (op == 4)
                 {
                     Cancelar_consulta();

                 }
                 if (op == 5)
                 {
                     Relatorio();
                 }

                 if (op == 6)
                 {

                     Consulta_Médico();
                 }
                 if (op == 7)
                 {
                     info();
                 }



             } while (op != 8);


          

                Console.ReadKey();



            }
        }
    }







