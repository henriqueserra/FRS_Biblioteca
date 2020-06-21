using PushbulletSharp;
using PushbulletSharp.Models.Requests;
using PushbulletSharp.Models.Responses;
using System.Configuration;

namespace FRS_Biblioteca
{
    public class Notificacao
    {
        public static void Envianotificação(string texto)
        {
            FRS_Biblioteca.DB.GravaAtividade(texto);
            var configuracao = ConfigurationManager.AppSettings;
            bool admin = false;

            if (configuracao["Mensagem para Admin"] == "Yes")
            {
                admin = true;
            }
            bool grupoadmin = (FRS_Biblioteca.GlobalVar.UsuarioLogado.grupo != "Admin");

            //if ((admin && grupoadmin) || (!grupoadmin))
            //{
            //    configuracao = ConfigurationManager.AppSettings;
            //    PushbulletClient client = new PushbulletClient("o.ijhfmbKRI8JAAAjaorTvs3n1I1oHo4qH");
            //    if (configuracao["celular1"] == "Yes")
            //    {
            //        PushNoteRequest request1 = new PushNoteRequest
            //        {
            //            DeviceIden = "ujy7mvFtukmsjE5NB4jvkO",
            //            Title = texto,
            //            Body = texto
            //        };

            //        PushResponse response1 = client.PushNote(request1);
            //    }

            //    if (configuracao["celular2"] == "Yes")
            //    {
            //        PushNoteRequest request2 = new PushNoteRequest
            //        {
            //            DeviceIden = "ujy7mvFtukmsjBmQj6ywIS",
            //            Title = texto,
            //            Body = texto
            //        };
            //        PushResponse response2 = client.PushNote(request2);
            //    }
            //}
        }
    }
}