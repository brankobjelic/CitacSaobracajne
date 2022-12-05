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
        AllData data;

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
            TBDrzavaIzdavanja.Text = "";
            TBDokumentIzdao.Text = "";
            TBDatumIzdavanja.Text = "";
            TBVaziDo.Text = "";
            TBZabranaOtudjenja.Text = "";
            TBBrojDokumenta.Text = "";
            TBSerijskiBroj.Text = "";
            TBVlasnik.Text = "";
            TBImeVlasnika.Text = "";
            TBJMBGVlasnika.Text = "";
            TBAdresaVlasnika.Text = "";
            TBKorisnik.Text = "";
            TBImeKorisnika.Text = "";
            TBJMBGKorisnika.Text = "";
            TBAdreasKorisnika.Text = "";
            TBRegistarskiBroj.Text = "";
            TBDatumPrveRegistracije.Text = "";
            TBMarka.Text = "";
            TBModel.Text = "";
            TBGodinaProizvodnje.Text = "";
            TBTip.Text = "";
            TBBoja.Text = "";
            TBBrojSasije.Text = "";
            TBBrojMotora.Text = "";
            TBSnagaMotora.Text = "";
            TBMasa.Text = "";
            TBZapreminaMotora.Text = "";
            TBNosivostVozila.Text = "";
            TBOdnosSnagaMasa.Text = "";
            TBBrojOsovina.Text = "";
            TBNajvecaDozvoljenaMasa.Text = "";
            TBBrojMestaZaSedenje.Text = "";
            TBBrojMestaZaStajanje.Text = "";
            TBHomologacijskaOznaka.Text = "";
            TBVrstaVozila.Text = "";
            TBPogonskoGorivo.Text = "";

            TBStatusBar.Text = "Započinjem čitanje...";
            ReaderDescriptor descriptor = (ReaderDescriptor)CBReaderSelector.SelectedValue;
            try
            {
                data = SaobracajnaReader.ReadAll(descriptor);
                TBDrzavaIzdavanja.Text = data.DocumentData.StateIssuing;
                TBDokumentIzdao.Text = data.DocumentData.AuthorityIssuing + ", " + data.DocumentData.CompetentAuthority;
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
            finally
            {
                TBStatusBar.Text = "Kraj čitanja podataka!";
            }
        }

        private void PrintButtonClickHandler(object sender, RoutedEventArgs e)
        {
            PrintWindow printWindow = new PrintWindow();
            printWindow.TBRegistarskaOznaka.Text = data.VehicleData.RegistrationNumberOfVehicle;
            printWindow.TBDatumIzdavanja.Text = data.DocumentData.IssuingDate;
            printWindow.TBSaobracajnuIzdao.Text = data.DocumentData.StateIssuing;
            printWindow.TBIzdavalac.Text = data.DocumentData.AuthorityIssuing + ",";
            printWindow.TBKompetent.Text = data.DocumentData.CompetentAuthority;
            printWindow.TBVaziDo.Text = data.DocumentData.ExpiryDate;
            printWindow.TBZabranaOtudjenja.Text = data.VehicleData.RestrictionToChangeOwner;
            printWindow.TBBrojSaobracajne.Text = data.DocumentData.UnambiguousNumber;
            printWindow.TBSerijskiBroj.Text = data.DocumentData.SerialNumber;
            printWindow.TBVlasnik.Text = data.PersonalData.OwnersSurnameOrBusinessName;
            printWindow.TBImeVlasnika.Text = data.PersonalData.OwnerName;
            printWindow.TBAdresaVlasnika.Text = data.PersonalData.OwnerAddress;
            printWindow.TBJMBGVlasnika.Text = data.PersonalData.OwnersPersonalNo;
            printWindow.TBKorisnik.Text = data.PersonalData.UsersSurnameOrBusinessName;
            printWindow.TBImeKorisnika.Text = data.PersonalData.UsersName;
            printWindow.TBAdresaKorisnika.Text = data.PersonalData.UsersAddress;
            printWindow.TBJMBGKorisnika.Text = data.PersonalData.UsersPersonalNo;
            printWindow.TBDatumPrveRegistracije.Text = data.VehicleData.DateOfFirstRegistration;
            printWindow.TBGodinaProizvodnje.Text = data.VehicleData.YearOfProduction;
            printWindow.TBMarka.Text = data.VehicleData.VehicleMake;
            printWindow.TBModel.Text = data.VehicleData.CommercialDescription;
            printWindow.TBTip.Text = data.VehicleData.VehicleType;
            printWindow.TBHomologacijskaOznaka.Text = data.VehicleData.TypeApprovalNumber;
            printWindow.TBBoja.Text = data.VehicleData.ColourOfVehicle;
            printWindow.TBBrojSasije.Text = data.VehicleData.VehicleIDNumber;
            printWindow.TBBrojMotora.Text = data.VehicleData.EngineIDNumber;
            printWindow.TBSnagaMotora.Text = data.VehicleData.MaximumNetPower;
            printWindow.TBOdnosSnagaMasa.Text = data.VehicleData.PowerWeightRatio;
            printWindow.TBKAtegorija.Text = data.VehicleData.VehicleCategory;
            printWindow.TBBrojOsovina.Text = data.VehicleData.NumberOfAxles;
            printWindow.TBZapreminaMotora.Text = data.VehicleData.EngineCapacity;
            printWindow.TBMasa.Text = data.VehicleData.VehicleMass;
            printWindow.TBNosivost.Text = data.VehicleData.VehicleLoad;
            printWindow.TBNajvecaDozvoljenaMasa.Text = data.VehicleData.MaximumPermissibleLadenMass;
            printWindow.TBPogonskoGorivo.Text = data.VehicleData.TypeOfFuel;
            printWindow.TBBrojMestaZaSedenje.Text = data.VehicleData.NumberOfSeats;
            printWindow.TBBrojMestaZaStajanje.Text = data.VehicleData.NumberOfStandingPlaces;
            printWindow.ShowDialog();
        }
    }
}
