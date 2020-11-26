using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Lab1
{
    public class Genetic
    {
        private string mobyDick= "ButhereisanartistHedesirestopaintyouthedreamiestshadiestquietestmostenchantingbitofromanticlandscapeinallthevalleyoftheSacoWhatisthechiefelementheemploysTherestandhistreeseachwithahollowtrunkasifahermitandacrucifixwerewithinandheresleepshismeadowandtheresleephiscattleandupfromyondercottagegoesasleepysmokeDeepintodistantwoodlandswindsamazywayreachingtooverlappingspursofmountainsbathedintheirhillsideblueButthoughthepictureliesthustrancedandthoughthispinetreeshakesdownitssighslikeleavesuponthisshepherdsheadyetallwerevainunlesstheshepherdseyewerefixeduponthemagicstreambeforehimGovisitthePrairiesinJunewhenforscoresonscoresofmilesyouwadekneedeepamongTigerlilieswhatistheonecharmwantingWaterthereisnotadropofwaterthereWereNiagarabutacataractofsandwouldyoutravelyourthousandmilestoseeitWhydidthepoorpoetofTennesseeuponsuddenlyreceivingtwohandfulsofsilverdeliberatewhethertobuyhimacoatwhichhesadlyneededorinvesthismoneyinapedestriantriptoRockawayBeachWhyisalmosteveryrobusthealthyboywitharobusthealthysoulinhimatsometimeorothercrazytogotoseaWhyuponyourfirstvoyageasapassengerdidyouyourselffeelsuchamysticalvibrationwhenfirsttoldthatyouandyourshipwerenowoutofsightoflandWhydidtheoldPersiansholdtheseaholyWhydidtheGreeksgiveitaseparatedeityandownbrotherofJoveSurelyallthisisnotwithoutmeaningAndstilldeeperthemeaningofthatstoryofNarcissuswhobecausehecouldnotgraspthetormentingmildimagehesawinthefountainplungedintoitandwasdrownedButthatsameimageweourselvesseeinallriversandoceansItistheimageoftheungraspablephantomoflifeandthisisthekeytoitallNowwhenIsaythatIaminthehabitofgoingtoseawheneverIbegintogrowhazyabouttheeyesandbegintobeoverconsciousofmylungsIdonotmeantohaveitinferredthatIevergotoseaasapassengerFortogoasapassengeryoumustneedshaveapurseandapurseisbutaragunlessyouhavesomethinginitBesidespassengersgetseasickgrowquarrelsomedontsleepofnightsdonotenjoythemselvesmuchasageneralthingnoInevergoasapassengernorthoughIamsomethingofasaltdoIevergotoseaasaCommodoreoraCaptainoraCookIabandonthegloryanddistinctionofsuchofficestothosewholikethemFormypartIabominateallhonourablerespectabletoilstrialsandtribulationsofeverykindwhatsoeverItisquiteasmuchasIcandototakecareofmyselfwithouttakingcareofshipsbarquesbrigsschoonersandwhatnotAndasforgoingascookthoughIconfessthereisconsiderablegloryinthatacookbeingasortofofficeronshipboardyetsomehowIneverfanciedbroilingfowlsthoughoncebroiledjudiciouslybutteredandjudgmaticallysaltedandpepperedthereisnoonewhowillspeakmorerespectfullynottosayreverentiallyofabroiledfowlthanIwillItisoutoftheidolatrousdotingsoftheoldEgyptiansuponbroiledibisandroastedriverhorsethatyouseethemummiesofthosecreaturesintheirhugebakehousesthepyramidsNowhenIgotoseaIgoasasimplesailorrightbeforethemastplumbdownintotheforecastlealofttheretotheroyalmastheadTruetheyratherordermeaboutsomeandmakemejumpfromspartosparlikeagrasshopperinaMaymeadowAndatfirstthissortofthingisunpleasantenoughIttouchesonessenseofhonourparticularlyifyoucomeofanoldestablishedfamilyinthelandtheVanRensselaersorRandolphsorHardicanutesAndmorethanallifjustprevioustoputtingyourhandintothetarpotyouhavebeenlordingitasacountryschoolmastermakingthetallestboysstandinaweofyouThetransitionisakeenoneIassureyoufromaschoolmastertoasailorandrequiresastrongdecoctionofSenecaandtheStoicstoenableyoutogrinandbearitButeventhiswearsoffintimeWhatofitifsomeoldhunksofaseacaptainordersmetogetabroomandsweepdownthedecksWhatdoesthatindignityamounttoweighedImeaninthescalesoftheNewTestamentDoyouthinkthearchangelGabrielthinksanythingthelessofmebecauseIpromptlyandrespectfullyobeythatoldhunksinthatparticularinstanceWhoaintaslaveTellmethatWellthenhowevertheoldseacaptainsmayordermeabouthowevertheymaythumpandpunchmeaboutIhavethesatisfactionofknowingthatitisallrightthateverybodyelseisonewayorotherservedinmuchthesamewayeitherinaphysicalormetaphysicalpointofviewthatisandsotheuniversalthumpispassedroundandallhandsshouldrubeachothersshoulderbladesandbecontentAgainIalwaysgotoseaasasailorbecausetheymakeapointofpayingmeformytroublewhereastheyneverpaypassengersasinglepennythatIeverheardofOnthecontrarypassengersthemselvesmustpayAndthereisallthedifferenceintheworldbetweenpayingandbeingpaidTheactofpayingisperhapsthemostuncomfortableinflictionthatthetwoorchardthievesentaileduponusButBEINGPAIDwhatwillcomparewithitTheurbaneactivitywithwhichamanreceivesmoneyisreallymarvellousconsideringthatwesoearnestlybelievemoneytobetherootofallearthlyillsandthatonnoaccountcanamoniedmanenterheavenAhhowcheerfullyweconsignourselvestoperditionFinallyIalwaysgotoseaasasailorbecauseofthewholesomeexerciseandpureairoftheforecastledeckForasinthisworldheadwindsarefarmoreprevalentthanwindsfromasternthatisifyouneverviolatethePythagoreanmaximsoforthemostparttheCommodoreonthequarterdeckgetshisatmosphereatsecondhandfromthesailorsontheforecastleHethinkshebreathesitfirstbutnotsoInmuchthesamewaydothecommonaltyleadtheirleadersinmanyotherthingsatthesametimethattheleaderslittlesuspectitButwhereforeitwasthatafterhavingrepeatedlysmelttheseaasamerchantsailorIshouldnowtakeitintomyheadtogoonawhalingvoyagethistheinvisiblepoliceofficeroftheFateswhohastheconstantsurveillanceofmeandsecretlydogsmeandinfluencesmeinsomeunaccountablewayhecanbetteranswerthananyoneelseAnddoubtlessmygoingonthiswhalingvoyageformedpartofthegrandprogrammeofProvidencethatwasdrawnupalongtimeagoItcameinasasortofbriefinterludeandsolobetweenmoreextensiveperformancesItakeitthatthispartofthebillmusthaverunsomethinglikethisGRANDCONTESTEDELECTIONFORTHEPRESIDENCYOFTHEUNITEDSTATESWHALINGVOYAGEBYONEISHMAELBLOODYBATTLEINAFFGHANISTANThoughIcannottellwhyitwasexactlythatthosestagemanagerstheFatesputmedownforthisshabbypartofawhalingvoyagewhenothersweresetdownformagnificentpartsinhightragediesandshortandeasypartsingenteelcomediesandjollypartsinfarcesthoughIcannottellwhythiswasexactlyyetnowthatIrecallallthecircumstancesIthinkIcanseealittleintothespringsandmotiveswhichbeingcunninglypresentedtomeundervariousdisguisesinducedmetosetaboutperformingthepartIdidbesidescajolingmeintothedelusionthatitwasachoiceresultingfrommyownunbiasedfreewillanddiscriminatingjudgmentChiefamongthesemotiveswastheoverwhelmingideaofthegreatwhalehimselfSuchaportentousandmysteriousmonsterrousedallmycuriosityThenthewildanddistantseaswhereherolledhisislandbulktheundeliverablenamelessperilsofthewhalethesewithalltheattendingmarvelsofathousandPatagoniansightsandsoundshelpedtoswaymetomywishWithothermenperhapssuchthingswouldnothavebeeninducementsbutasformeIamtormentedwithaneverlastingitchforthingsremoteIlovetosailforbiddenseasandlandonbarbarouscoastsNotignoringwhatisgoodIamquicktoperceiveahorrorandcouldstillbesocialwithitwouldtheyletmesinceitisbutwelltobeonfriendlytermswithalltheinmatesoftheplaceonelodgesinByreasonofthesethingsthenthewhalingvoyagewaswelcomethegreatfloodgatesofthewonderworldswungopenandinthewildconceitsthatswayedmetomypurposetwoandtwotherefloatedintomyinmostsoulendlessprocessionsofthewhaleandmidmostofthemallonegrandhoodedphantomlikeasnowhillintheairCHAPTERTheCarpetBagIstuffedashirtortwointomyoldcarpetbagtuckeditundermyarmandstartedforCapeHornandthePacificQuittingthegoodcityofoldManhattoIdulyarrivedinNewBedfordItwasaSaturdaynightinDecemberMuchwasIdisappointeduponlearningthatthelittlepacketforNantuckethadalreadysailedandthatnowayofreachingthatplacewouldoffertillthefollowingMondayAsmostyoungcandidatesforthepainsandpenaltiesofwhalingstopatthissameNewBedfordthencetoembarkontheirvoyageitmayaswellberelatedthatIforonehadnoideaofsodoingFormymindwasmadeuptosailinnootherthanaNantucketcraftbecausetherewasafineboisteroussomethingabouteverythingconnectedwiththatfamousoldislandwhichamazinglypleasedmeBesidesthoughNewBedfordhasoflatebeengraduallymonopolisingthebusinessofwhalingandthoughinthismatterpooroldNantucketisnowmuchbehindheryetNantucketwashergreatoriginaltheTyreofthisCarthagetheplacewherethefirstdeadAmericanwhalewasstrandedWhereelsebutfromNantucketdidthoseaboriginalwhalementheRedMenfirstsallyoutincanoestogivechasetotheLeviathanAndwherebutfromNantuckettoodidthatfirstadventurouslittlesloopputforthpartlyladenwithimportedcobblestonessogoesthestorytothrowatthewhalesinordertodiscoverwhentheywerenighenoughtoriskaharpoonfromthebowspritNowhavinganightadayandstillanothernightfollowingbeforemeinNewBedfordereIcouldembarkformydestinedportitbecameamatterofconcernmentwhereIwastoeatandsleepmeanwhileItwasaverydubiouslookingnayaverydarkanddismalnightbitinglycoldandcheerlessIknewnooneintheplaceWithanxiousgrapnelsIhadsoundedmypocketandonlybroughtupafewpiecesofsilverSowhereveryougoIshmaelsaidItomyselfasIstoodinthemiddleofadrearystreetshoulderingmybagandcomparingthegloomtowardsthenorthwiththedarknesstowardsthesouthwhereverinyourwisdomyoumayconcludetolodgeforthenightmydearIshmaelbesuretoinquirethepriceanddontbetooparticularWithhaltingstepsIpacedthestreetsandpassedthesignofTheCrossedHarpoonsbutitlookedtooexpensiveandjollythereFurtheronfromthebrightredwindowsoftheSwordFishInntherecamesuchferventraysthatitseemedtohavemeltedthepackedsnowandicefrombeforethehouseforeverywhereelsethecongealedfrostlayteninchesthickinahardasphalticpavementratherwearyformewhenIstruckmyfootagainsttheflintyprojectionsbecausefromhardremorselessservicethesolesofmybootswereinamostmiserableplightTooexpensiveandjollyagainthoughtIpausingonemomenttowatchthebroadglareinthestreetandhearthesoundsofthetinklingglasseswithinButgoonIshmaelsaidIatlastdontyouheargetawayfrombeforethedooryourpatchedbootsarestoppingthewaySoonIwentInowbyinstinctfollowedthestreetsthattookmewaterwardfortheredoubtlesswerethecheapestifnotthecheeriestinnsSuchdrearystreetsblocksofblacknessnothousesoneitherhandandhereandthereacandlelikeacandlemovingaboutinatombAtthishourofthenightofthelastdayoftheweekthatquarterofthetownprovedallbutdesertedButpresentlyIcametoasmokylightproceedingfromalowwidebuildingthedoorofwhichstoodinvitinglyopenIthadacarelesslookasifitweremeantfortheusesofthepublicsoenteringthefirstthingIdidwastostumbleoveranashboxintheporchHathoughtIhaastheflyingparticlesalmostchokedmearetheseashesfromthatdestroyedcityGomorrahButTheCrossedHarpoonsandTheSwordFishthisthenmustneedsbethesignofTheTrapHoweverIpickedmyselfupandhearingaloudvoicewithinpushedonandopenedasecondinteriordoorItseemedthegreatBlackParliamentsittinginTophetAhundredblackfacesturnedroundintheirrowstopeerandbeyondablackAngelofDoomwasbeatingabookinapulpitItwasanegrochurchandthepreacherstextwasabouttheblacknessofdarknessandtheweepingandwailingandteethgnashingthereHaIshmaelmutteredIbackingoutWretchedentertainmentatthesignofTheTrapMovingonIatlastcametoadimsortoflightnotfarfromthedocksandheardaforlorncreakingintheairandlookingupsawaswingingsignoverthedoorwithawhitepaintinguponitfaintlyrepresentingatallstraightjetofmistysprayandthesewordsunderneathTheSpouterInnPeterCoffinCoffinSpouterRatherominousinthatparticularconnexionthoughtIButitisacommonnameinNantuckettheysayandIsupposethisPeterhereisanemigrantfromthereAsthelightlookedsodimandtheplaceforthetimelookedquietenoughandthedilapidatedlittlewoodenhouseitselflookedasifitmighthavebeencartedherefromtheruinsofsomeburntdistrictandastheswingingsignhadapovertystrickensortofcreaktoitIthoughtthatherewastheveryspotforcheaplodgingsandthebestofpeacoffeeItwasaqueersortofplaceagableendedoldhouseonesidepalsiedasitwereandleaningoversadlyItstoodonasharpbleakcornerwherethattempestuouswindEuroclydonkeptupaworsehowlingthaneveritdidaboutpoorPaulstossedcraftEuroclydonneverthelessisamightypleasantzephyrtoanyoneindoorswithhisfeetonthehobquietlytoastingforbedInjudgingofthattempestuouswindcalledEuroclydonsaysanoldwriterofwhoseworksIpossesstheonlycopyextantitmakethamarvellousdifferencewhetherthoulookestoutatitfromaglasswindowwherethefrostisallontheoutsideorwhetherthouobservestitfromthatsashlesswindowwherethefrostisonbothsidesandofwhichthewightDeathistheonlyglazierTrueenoughthoughtIasthispassageoccurredtomymindoldblackletterthoureasonestwellYestheseeyesarewindowsandthisbodyofmineisthehouseWhatapitytheydidntstopupthechinksandthecranniesthoughandthrustinalittlelinthereandthereButitstoolatetomakeanyimprovementsnowTheuniverseisfinishedthecopestoneisonandthechipswerecartedoffamillionyearsagoPoorLazarustherechatteringhisteethagainstthecurbstoneforhispillowandshakingoffhistatterswithhisshiveringshemightplugupbothearswithragsandputacorncobintohismouthandyetthatwouldnotkeepoutthetempestuousEuroclydonEuroclydonsaysoldDivesinhisredsilkenwrapperhehadaredderoneafterwardspoohpoohWhatafinefrostynighthowOrionglitterswhatnorthernlightsLetthemtalkoftheirorientalsummerclimesofeverlastingconservatoriesgivemetheprivilegeofmakingmyownsummerwithmyowncoalsButwhatthinksLazarusCanhewarmhisbluehandsbyholdingthemuptothegrandnorthernlightsWouldnotLazarusratherbeinSumatrathanhereWouldhenotfarratherlayhimdownlengthwisealongthelineoftheequatoryeayegodsgodowntothefierypititselfinordertokeepoutthisfrostNowthatLazarusshouldliestrandedthereonthecurbstonebeforethedoorofDivesthisismorewonderfulthanthatanicebergshouldbemooredtooneoftheMoluccasYetDiveshimselfhetooliveslikeaCzarinanicepalacemadeoffrozensighsandbeingapresidentofatemperancesocietyheonlydrinksthetepidtearsoforphansButnomoreofthisblubberingnowwearegoingawhalingandthereisplentyofthatyettocomeLetusscrapetheicefromourfrostedfeetandseewhatsortofaplacethisSpoutermaybeCHAPTERTheSpouterInnEnteringthatgableendedSpouterInnyoufoundyourselfinawidelowstragglingentrywitholdfashionedwainscotsremindingoneofthebulwarksofsomecondemnedoldcraftOnonesidehungaverylargeoilpaintingsothoroughlybesmokedandeverywaydefacedthatintheunequalcrosslightsbywhichyouviewedititwasonlybydiligentstudyandaseriesofsystematicvisitstoitandcarefulinquiryoftheneighborsthatyoucouldanywayarriveatanunderstandingofitspurposeSuchunaccountablemassesofshadesandshadowsthatatfirstyoualmostthoughtsomeambitiousyoungartistinthetimeoftheNewEnglandhagshadendeavoredtodelineatechaosbewitchedButbydintofmuchandearnestcontemplationandoftrepeatedponderingsandespeciallybythrowingopenthelittlewindowtowardsthebackoftheentryyouatlastcometotheconclusionthatsuchanideahoweverwildmightnotbealtogetherunwarrantedButwhatmostpuzzledandconfoundedyouwasalonglimberportentousblackmassofsomethinghoveringinthecentreofthepictureoverthreebluedimperpendicularlinesfloatinginanamelessyeastAboggysoggysquitchypicturetrulyenoughtodriveanervousmandistractedYetwasthereasortofindefinitehalfattainedunimaginablesublimityaboutitthatfairlyfrozeyoutoittillyouinvoluntarilytookanoathwithyourselftofindoutwhatthatmarvellouspaintingmeantEverandanonabrightbutalasdeceptiveideawoulddartyouthroughItstheBlackSeainamidnightgaleItstheunnaturalcombatofthefourprimalelementsItsablastedheathItsaHyperboreanwintersceneItsthebreakingupoftheiceboundstreamofTimeButatlastallthesefanciesyieldedtothatoneportentoussomethinginthepicturesmidstTHAToncefoundoutandalltherestwereplainButstopdoesitnotbearafaintresemblancetoagiganticfisheventhegreatleviathanhimself".ToUpper();
        private char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private string cyphrotext = "";
        private Dictionary<string, double> threegramIndexes = new Dictionary<string, double>();
        private double expectedIndex = -3.5939192291735513;
        public Genetic()
        {
            ReadThreegrams();
            cyphrotext = File.ReadAllText(@".\..\..\..\text3.txt");

            //the next one command can be used to generate expectedIndex based on first 3 
            //chapters of moby dick
            //expectedIndex = GetThreegramsIndex(test);
        }

        /// <summary>
        /// Read all etalon 3-grams from "3grams.txt" and make threegramIndexes from it
        /// </summary>
        public void ReadThreegrams()
        {
            Dictionary<string, decimal> threegramsCounts = new Dictionary<string, decimal>();
            decimal sum = 0;
            string[] text = File.ReadAllLines(@".\..\..\..\3grams.txt");
            foreach(var line in text)
            {
                string key = line.Substring(0, 3);
                decimal value = decimal.Parse(line.Substring(5, line.Length - 5));
                threegramsCounts.Add(key.ToUpper(), value);
                sum += value;
            }
            foreach(var keyValue in threegramsCounts)
            {
                double count = Decimal.ToDouble(Decimal.Divide(keyValue.Value, sum));
                threegramIndexes.Add(keyValue.Key, Math.Log10(count));
                //Console.WriteLine(keyValue + "   " + Math.Log10(count) + "   ");
            }
        }

        /// <summary>
        /// Get 3-gram index for text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public double GetThreegramsIndex(string text)
        {
            double index = 0;
            for(int i = 0; i < text.Length-2; i++)
            {
                index+= threegramIndexes[text.Substring(i, 3)];
            }
            return index/(text.Length -2);
        }

        /// <summary>
        /// estimate individual using 3-gram indexes
        /// </summary>
        /// <param name="populationItem"></param>
        /// <returns></returns>
        private double EstimateBasedOnThreegrams(string populationItem)
        {
            double index = GetThreegramsIndex(DecryptSubstitution(cyphrotext, populationItem));
            double estimation = Math.Abs(index - expectedIndex);
            return estimation;
        }

        /// <summary>
        /// Decryptes cybertext using genetic algorithm
        /// </summary>
        /// <returns></returns>
        public string Decrypt()
        {
            int generation = 0;
            List<string> population = GetFirstPopulation(1000);
            string best = GetBest(population, 1)[0];
            while(EstimateBasedOnThreegrams(best) >= 0.12)
            { 
                Console.WriteLine(generation);
                List<string> bestFromPopulation = GetBest(population, 500);
                List<string> children = Crossing(bestFromPopulation);
                MutatePopulation(children);
                population = children;
                generation++;
                best = GetBest(population, 1)[0];
                Console.WriteLine("estimation = " + EstimateBasedOnThreegrams(best));
                Console.WriteLine(DecryptSubstitution(cyphrotext, best));
            }
            string decrypted = DecryptSubstitution(cyphrotext, GetBest(population, 1)[0]);
            return decrypted;
        }

        /// <summary>
        /// Decryptes substitution cipher
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string DecryptSubstitution(string cipherText, string key)
        {
            char[] chars = new char[cipherText.Length];

            for (int i = 0; i < cipherText.Length; i++)
            {
                if (key.IndexOf(cipherText[i]) == -1)
                    Console.WriteLine("vse och pogano");
                int j = key.IndexOf(cipherText[i]) + 65;
                chars[i] = (char)j;
            }
            return new string(chars);
        }

        /// <summary>
        /// Gets item for first population
        /// </summary>
        /// <returns></returns>
        private string GetFirstPopulationItem()
        {
            StringBuilder item = new StringBuilder();
            Random random = new Random();
            char letter;
            for (int i = 0; i < alphabet.Length; i++)
            {
                //there shouldn`t be one letter twice
                do
                {
                    int index = random.Next(alphabet.Length);
                    letter = alphabet[index];
                } while (item.ToString().Contains(letter));
                item.Append(letter);
            }
            return item.ToString();
        }

        /// <summary>
        /// Gets first population for genetic algorithm
        /// </summary>
        /// <param name="populationLength"></param>
        /// <returns></returns>
        private List<string> GetFirstPopulation(int populationLength)
        {
            List<string> population = new List<string>();
            for (int i = 0; i < populationLength; i++)
            {
                string item = GetFirstPopulationItem();
                population.Add(item);
            }
            return population;
        }

        /// <summary>
        /// Get estimates for population using 3-grams 
        /// </summary>
        /// <param name="population"></param>
        /// <returns></returns>
        private List<double> EstimatePopulation(List<string> population)
        {
            List<double> estimates = new List<double>();
            foreach (var item in population)
            {
                estimates.Add(EstimateBasedOnThreegrams(item));
            }
            return estimates;
        }

        /// <summary>
        /// Get the best n individuals from population
        /// where n is aliveCount
        /// </summary>
        /// <param name="population"></param>
        /// <param name="aliveCount"></param>
        /// <returns></returns>
        private List<string> GetBest(List<string> population, int aliveCount)
        {
            List<string> bestItems = new List<string>();
            List<double> estimates = EstimatePopulation(population);
            for (int i = 0; i < aliveCount; i++)
            {
                int minIndex = GetMinIndex(estimates);

                bestItems.Add(population[minIndex]);
                estimates.RemoveAt(minIndex);
            }
            return bestItems;
        }

        /// <summary>
        /// Get index of minimal element in list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int GetMinIndex(List<double> list)
        {
            int index = 0;
            double min = double.MaxValue;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] < min)
                {
                    min = list[i];
                    index = i;
                }
            }
            return index;
        }

        /// <summary>
        /// make as much children as parents and add them into population
        /// </summary>
        /// <param name="bestFromPopulation"></param>
        /// <returns></returns>
        private List<string> Crossing(List<string> bestFromPopulation)
        {
            List<string> childs = new List<string>();
            for (int i = 1; i < bestFromPopulation.Count * 2; i++)
            {
                Random random = new Random();
                int index1 = random.Next(bestFromPopulation.Count);
                int index2 = random.Next(bestFromPopulation.Count);
                while(index1 == index2)
                    index2 = random.Next(bestFromPopulation.Count);
                childs.Add(Cross(bestFromPopulation[index1], bestFromPopulation[index2]));
            }
            bestFromPopulation.AddRange(childs);
            return bestFromPopulation;
        }

        /// <summary>
        /// Mutate every in individual with 10% probability
        /// </summary>
        /// <param name="population"></param>
        private void MutatePopulation(List<string> population)
        {
            for (int i = 0; i < population.Count; i++)
            {
                Random random = new Random();

                int rnd = random.Next(100);
                if (rnd <= 10)
                {
                    population[i] = Mutate(population[i]);
                }
            }
        }

        /// <summary>
        /// Mutate individual by swap 2 elements inside it
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string Mutate(string item)
        {
            Random random = new Random();
            int index1 = random.Next(item.Length);
            int index2 = random.Next(item.Length);
            item = Swap(index1, index2, item);
            return item;
        }

        /// <summary>
        /// swap chars in string
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private string Swap(int index1, int index2, string item)
        {
            StringBuilder sb = new StringBuilder(item);
            char temp = sb[index1];
            sb[index1] = sb[index2];
            sb[index2] = temp;
            return sb.ToString();
        }

        /// <summary>
        /// Make child from 2 parents. Child have gene from mom or dad randomly
        /// </summary>
        /// <param name="firstParent"></param>
        /// <param name="secondParent"></param>
        /// <returns></returns>
        private string Cross(string firstParent, string secondParent)
        {
            StringBuilder child = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < firstParent.Length; i++)
            {
                int parentNumber = random.Next(2);
                if (child.ToString().Contains(firstParent[i]) && child.ToString().Contains(secondParent[i]))
                {
                    child = MakeAnotherDecisionInPast(child, firstParent, secondParent, i);
                }

                if (child.ToString().Contains(firstParent[i]))
                {
                    child.Append(secondParent[i]);
                }
                else if (child.ToString().Contains(secondParent[i]))
                {
                    child.Append(firstParent[i]);
                }
                else if (parentNumber == 0)
                {
                    child.Append(firstParent[i]);
                    
                }
                else
                {
                    child.Append(secondParent[i]);
                }
            }

           return child.ToString();
        }

        /// <summary>
        /// This function is used when child contains firstParent[index] and secondParent[index]. 
        /// Than we recursively make another dicisions in past
        /// </summary>
        /// <param name="child"></param>
        /// <param name="firstParent"></param>
        /// <param name="secondParent"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private StringBuilder MakeAnotherDecisionInPast(StringBuilder child, string firstParent, string secondParent, int index)
        {
            if (!child.ToString().Contains(firstParent[index]))
            {
                return child;
            }
            int index1 = child.ToString().IndexOf(firstParent[index]);
            child[index1] = ' ';
            child = MakeAnotherDecisionInPast(child, firstParent, secondParent, index1);
            child[index1] = firstParent[index1];
            return child;
        }
    }
}
