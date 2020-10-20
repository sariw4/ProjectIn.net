using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using BE;
using System.Net;
using System.Threading;

namespace DAL
{
    public class dal_imp_try : IDal
    {
        //Singelton
        static dal_imp_try instance = new dal_imp_try();
        public static dal_imp_try Instance { get { return instance; } }
        string solutionDirectory;// = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
        string filePath;// = System.IO.Path.Combine(solutionDirectory, "Data");

        //Roots and paths of the files

        XElement OrderRoot;
        XElement ConfigurationRoot;
        //string GuestRequestRootPath = @"XMLGuestRequest.xml";
        //string HostingUnitRootPath = @"XMLHostingUnit.xml";
        //string OrderRootPath = @"XMLOrder.xml";
        //string ConfigurationRootPath = @"XMLConfiguration.xml";
        string GuestRequestRootPath;// = Path.Combine(filePath, "XMLGuestRequest.xml");
        string HostingUnitRootPath;// = Path.Combine(filePath, "XMLHostingUnit.xml");
        string OrderRootPath;// = Path.Combine(filePath, "XMLOrder.xml");
        string ConfigurationRootPath;// = Path.Combine(filePath, "XMLConfiguration.xml");

        XElement banksRoot;
        string banksPath;

        //ctor
        public dal_imp_try()
        {
            solutionDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
            filePath = System.IO.Path.Combine(solutionDirectory, "dataFolder");
            GuestRequestRootPath = Path.Combine(filePath, "XMLGuestRequest.xml");
            HostingUnitRootPath = Path.Combine(filePath, "XMLHostingUnit.xml");
            OrderRootPath = Path.Combine(filePath, "XMLOrder.xml");
            ConfigurationRootPath = Path.Combine(filePath, "XMLConfiguration.xml");
            banksPath = Path.Combine(filePath, "atm.xml");
            if (!File.Exists(banksPath))
            {
                try
                {
                    downloadAtm();
                    new Thread(downloadAtm).Start();
                    while (!File.Exists(banksPath))
                    {
                        Thread.Sleep(1000 * 10);
                    }
                    banksRoot = XElement.Load(banksPath);
                }
                catch
                {
                    throw new FileLoadException("file upload problem");
                }
            }
            else
            {
                banksRoot = XElement.Load(banksPath);
            }
            //check if files are exist
            if (!File.Exists(GuestRequestRootPath))
            {
                FileStream file = new FileStream(GuestRequestRootPath, FileMode.Create);
                file.Close();

            }
            if (!File.Exists(HostingUnitRootPath))
            {
                FileStream file = new FileStream(HostingUnitRootPath, FileMode.Create);
                file.Close();
            }
            if (!File.Exists(OrderRootPath))
            {
                OrderRoot = new XElement("Orders");
                OrderRoot.Save(OrderRootPath);
            }
            else
            {
                try
                {
                    OrderRoot = XElement.Load(OrderRootPath);
                }
                catch
                {
                    throw new FileLoadException("File uploading went wrong");
                }
            }

            if (!File.Exists(ConfigurationRootPath))
            {
                ConfigurationRoot = new XElement("Configuration");
                ConfigurationRoot.Add(new XElement("GuestRequestKeySeq", 10000000));
                ConfigurationRoot.Add(new XElement("HostingUnitKeySeq", 10000000));
                ConfigurationRoot.Add(new XElement("OrderKeySeq", 10000000));
                ConfigurationRoot.Add(new XElement("HostKeySeq", 10000000));
                ConfigurationRoot.Add(new XElement("Manager", 309090));
                ConfigurationRoot.Add(new XElement("Fee", 10));
                ConfigurationRoot.Save(ConfigurationRootPath);
            }
            else
            {
                try
                {
                    ConfigurationRoot = XElement.Load(ConfigurationRootPath);
                }
                catch
                {

                    throw new FileLoadException("File uploading went wrong");
                }
            }
        }
        void downloadAtm()
        {
            XElement atm;
            try
            {
                atm = new XElement("ATMs");
                atm = XElement.Load("https://drive.google.com/u/0/uc?id=1FpcqslnRD6naLHOjrCvKArCg3Ihkb9hR&export=download");
                atm.Save(banksPath);
            }
            catch (Exception)
            {
                atm = new XElement("ATMs");
                atm = XElement.Load("http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml");
                atm.Save(banksPath);
            }
        }

        // generic save function
        public static void SaveToXML<T>(T source, string rootPath)
        {
            FileStream file = new FileStream(rootPath, FileMode.Create);
            XmlSerializer x = new XmlSerializer(source.GetType());
            x.Serialize(file, source);
            file.Close();
        }
        public static List<T> loadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                List<T> result = (List<T>)x.Deserialize(file);
                file.Close();
                return result;
            }
            catch
            {
                file.Close();
                return new List<T>();
            }

        }
        public void AddClientRequest(GuestRequest gr)
        {
            List<GuestRequest> GuestRequestlist = loadFromXML<GuestRequest>(GuestRequestRootPath);
            if (GuestRequestlist.Exists(item => item.GuestRequestKey == gr.GuestRequestKey) == true)
                throw new Exception("the guest request is already exists ");
            else
            {
                gr.Status = RequestStatus.Open;
                gr.RegistrationDate = DateTime.Now;
                long g = Convert.ToInt64(ConfigurationRoot.Element("GuestRequestKeySeq").Value);
                gr.GuestRequestKey = g++;
                ConfigurationRoot.Element("GuestRequestKeySeq").Value = g.ToString();
                ConfigurationRoot.Save(ConfigurationRootPath);
                GuestRequestlist.Add(gr);
                SaveToXML<List<GuestRequest>>(GuestRequestlist, GuestRequestRootPath);
            }
        }

        public void AddHostingUnit(HostingUnit ho)
        {
            List<HostingUnit> HostingunitList = loadFromXML<HostingUnit>(HostingUnitRootPath);
            if (HostingunitList.Exists(item => item.HostingUnitKey == ho.HostingUnitKey) == true)
                throw new Exception("the hosting unit is already exists ");
            else
            {
                long g = Convert.ToInt64(ConfigurationRoot.Element("HostingUnitKeySeq").Value);
                ho.HostingUnitKey = g++;
                ConfigurationRoot.Element("HostingUnitKeySeq").Value = g.ToString();
                ConfigurationRoot.Save(ConfigurationRootPath);
                bool[,] d = new bool[32, 13];
                for (int j = 0; j < 32; j++)
                {
                    for (int i = 0; i < 13; i++)
                        d[j, i] = false;
                }
                ho.Diary = d;
                HostingunitList.Add(ho);
                SaveToXML<List<HostingUnit>>(HostingunitList, HostingUnitRootPath);
            }

        }
        private void LoadOrder()
        {
            try
            {
                OrderRoot = XElement.Load(OrderRootPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }

        }
        private void LoadConfiguration()
        {
            try
            {
                ConfigurationRoot = XElement.Load(ConfigurationRootPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }

        }
        public void AddOrder(Order or)
        {
            try
            {
                LoadOrder();
                LoadConfiguration();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            or.OrderKey = Convert.ToInt64(ConfigurationRoot.Element("OrderKeySeq").Value);
            XElement HostingUnitKey = new XElement("HostingUnitKeySeq", or.HostingUnitKey);
            XElement GuestRequestKey = new XElement("GuestRequestKeySeq", or.GuestRequestKey);
            //XElement OrderKey = new XElement(ConfigurationRoot.Element("OrderKeySeq").Value, or.OrderKey);
            XElement OrderKey = new XElement("OrderKeySeq", or.OrderKey);
            XElement Status = new XElement("Status", or.Status);
            XElement CreateDate = new XElement("CreateDate", or.CreateDate);
            XElement OrderDate = new XElement("OrderDate", or.OrderDate);
            XElement Order = new XElement("Orders", HostingUnitKey, GuestRequestKey, OrderKey, Status, CreateDate, OrderDate);
            OrderRoot.Add(Order);
            long g = Convert.ToInt64(ConfigurationRoot.Element("OrderKeySeq").Value);
            g++;
            ConfigurationRoot.Element("OrderKeySeq").Value = g.ToString();
            ConfigurationRoot.Save(ConfigurationRootPath);
            OrderRoot.Save(OrderRootPath);
        }

        public List<GuestRequest> GetGuestRequests()
        {
            List<GuestRequest> GuestRequestlist = loadFromXML<GuestRequest>(GuestRequestRootPath);
            return GuestRequestlist;
        }

        public List<HostingUnit> GetHostingUnits()
        {
            List<HostingUnit> HostingunitList = loadFromXML<HostingUnit>(HostingUnitRootPath);
            return HostingunitList;
        }

        public List<Order> GetOrders()
        {
            LoadOrder();
            List<Order> orders;
            try
            {
                orders = (from p in OrderRoot.Elements()
                          select new Order()
                          {
                              HostingUnitKey = long.Parse(p.Element("HostingUnitKeySeq").Value),
                              GuestRequestKey = long.Parse(p.Element("GuestRequestKeySeq").Value),
                              CreateDate = DateTime.Parse(p.Element("CreateDate").Value),
                              OrderDate = DateTime.Parse(p.Element("OrderDate").Value),
                              OrderKey = long.Parse(p.Element("OrderKeySeq").Value),
                              Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), p.Element("Status").Value),
                          }).ToList();
                //long g = Convert.ToInt64(ConfigurationRoot.Element("OrderKeySeq").Value);
                //g++;
                //ConfigurationRoot.Element("OrderKeySeq").Value = g.ToString();
                //ConfigurationRoot.Save(ConfigurationRootPath);
            }
            catch
            {
                orders = null;
            }
            return orders;
        }

        public void RemoveHostingUnit(HostingUnit ho)
        {
            List<HostingUnit> HostingunitList = loadFromXML<HostingUnit>(HostingUnitRootPath);
            if (HostingunitList.Exists(item => item.HostingUnitKey == ho.HostingUnitKey) == false)
                throw new Exception("the hosting unit isn't exists ");
            else
                HostingunitList.Remove(ho);
            SaveToXML<List<HostingUnit>>(HostingunitList, HostingUnitRootPath);
        }

        //public List<BankBranch> ReturnBankBranch()
        //{
        //    throw new Exception();
        //}

        public void UpdateClientRequestStatus(GuestRequest gr, RequestStatus requestStatus)
        {
            List<GuestRequest> GuestRequestlist = loadFromXML<GuestRequest>(GuestRequestRootPath);
            var v = from item in GuestRequestlist
                    where item.GuestRequestKey == gr.GuestRequestKey
                    select item;
            if (v != null)
            {
                foreach (var item in v)
                {
                    if (requestStatus == item.Status)
                        throw new Exception("the status of guest request is already initialized to what you requested ");
                    else
                    {
                        gr.Status = requestStatus;
                        SaveToXML<List<GuestRequest>>(GuestRequestlist, GuestRequestRootPath);
                    }
                }
            }
            else
                throw new Exception("the status of guest request isn't exists");
        }

        public void UpdateHostingUnit(HostingUnit ho)
        {
            List<HostingUnit> HostingunitList = loadFromXML<HostingUnit>(HostingUnitRootPath);
            int x = (HostingunitList.FindIndex(item => item.HostingUnitKey == ho.HostingUnitKey));
            if (x != -1)
            {
                HostingunitList[x] = ho;
                SaveToXML<List<HostingUnit>>(HostingunitList, HostingUnitRootPath);
            }
            else { throw new Exception("the hosting unit isn't exists "); }
        }

        public void UpdateOrder(Order or, OrderStatus ortStatus)
        {
            XElement orderElement = (from item in OrderRoot.Elements()
                                     where or.OrderKey == Convert.ToInt64(item.Element("OrderKeySeq").Value)
                                     select item).FirstOrDefault();
            if (orderElement != null)
            {
                if (ortStatus.ToString() == orderElement.Element("Status").Value)
                    throw new Exception("the status of order is already initialized to what you requested ");

                else
                {
                    orderElement.Element("Status").Value = ortStatus.ToString();
                    OrderRoot.Save(OrderRootPath);
                }
            }
            else { throw new Exception("the order isn't exists "); }
        }

        BankBranch ConvertBankBranch(XElement element)
        {
            return new BankBranch()
            {
                BankNumber = int.Parse(element.Element("קוד_בנק").Value),
                BankName = element.Element("שם_בנק").Value,
                BranchNumber = int.Parse(element.Element("קוד_סניף").Value),
                BranchAddress = element.Element("כתובת_ה-ATM").Value,
                BranchCity = element.Element("ישוב").Value,
                //BankAccountNumber= long.Parse(element.Element("BankAccountNumber").Value)
            };
        }

        public IEnumerable<BankBranch> getAllBankBranches()
        {
            return (from item in banksRoot.Elements()
                    let a = ConvertBankBranch(item)
                    select a).GroupBy(x => (x.BranchAddress + x.BranchCity)).Select(x => x.First());
        }
    }
}
