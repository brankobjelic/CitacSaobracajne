using SaobracajnaNET;
using SaobracajnaNET.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CitacSaobracajne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Content_Rendered(object sender, EventArgs e)
        {
            try
            {
                var readers = SaobracajnaReader.GetAllReaders();
                CBReaderSelector.ItemsSource = readers;
            }
            catch (SaobracajnaNETException ex)
            {
                if(ex.ErrorCode == ScardError.SCARD_E_NO_READERS_AVAILABLE)
                {
                    MessageBox.Show("Nema povezanih citaca. Ok za zatvaranje programa");
                    Application.Current.Shutdown();
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

        private void ButtonClickHandler(object sender, RoutedEventArgs e)
        {
            ReaderDescriptor descriptor = (ReaderDescriptor)CBReaderSelector.SelectedValue;
            try
            {
                var data = SaobracajnaReader.ReadAll(descriptor);
                TBDrzavaIzdavanja.Text = data.DocumentData.StateIssuing;
                TBDokumentIzdao.Text = data.DocumentData.AuthorityIssuing;
                TBDatumIzdavanja.Text = data.DocumentData.IssuingDate;
                TBVaziDo.Text = data.DocumentData.ExpiryDate;
                TBZabranaOtudjenja.Text = data.VehicleData.RestrictionToChangeOwner;
                TBBrojDokumenta.Text = data.DocumentData.UnambiguousNumber;
                TBSerijskiBroj.Text = data.DocumentData.SerialNumber;
                TBVlasnik.Text = data.PersonalData.OwnersSurnameOrBusinessName;
                TBImeVlasnika.Text = data.PersonalData.OwnerName;
                TBJMBGVlasnika.Text = data.PersonalData.OwnersPersonalNo;
                TBAdresaVlasnika.Text = data.PersonalData.OwnerAddress;
                TBKorisnik.Text = data.PersonalData.UsersSurnameOrBusinessName;
                TBImeKorisnika.Text = data.PersonalData.UsersName;
                TBJMBGKorisnika.Text = data.PersonalData.UsersPersonalNo;
                TBAdreasKorisnika.Text = data.PersonalData.UsersAddress;
                TBRegistarskiBroj.Text = data.VehicleData.RegistrationNumberOfVehicle;
                TBDatumPrveRegistracije.Text = data.VehicleData.DateOfFirstRegistration;
                TBMarka.Text = data.VehicleData.VehicleMake;
                TBModel.Text = data.VehicleData.CommercialDescription;
                TBGodinaProizvodnje.Text = data.VehicleData.YearOfProduction;
                TBTip.Text = data.VehicleData.VehicleType;
                TBBoja.Text = data.VehicleData.ColourOfVehicle;
                TBBrojSasije.Text = data.VehicleData.VehicleIDNumber;
                TBBrojMotora.Text = data.VehicleData.EngineIDNumber;
                TBSnagaMotora.Text = data.VehicleData.MaximumNetPower;
                TBMasa.Text = data.VehicleData.VehicleMass;
                TBZapreminaMotora.Text = data.VehicleData.EngineCapacity;
                TBNosivostVozila.Text = data.VehicleData.VehicleLoad;
                TBOdnosSnagaMasa.Text = data.VehicleData.PowerWeightRatio;
                TBBrojOsovina.Text = data.VehicleData.NumberOfAxles;
                TBNajvecaDozvoljenaMasa.Text = data.VehicleData.MaximumPermissibleLadenMass;
                TBBrojMestaZaSedenje.Text = data.VehicleData.NumberOfSeats;
                TBBrojMestaZaStajanje.Text = data.VehicleData.NumberOfStandingPlaces;
                TBHomologacijskaOznaka.Text = data.VehicleData.TypeApprovalNumber;
                TBVrstaVozila.Text = data.VehicleData.VehicleCategory;
                TBPogonskoGorivo.Text = data.VehicleData.TypeOfFuel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
