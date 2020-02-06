namespace FRS_Biblioteca
    {
    public class Usuario
        {
        /// <summary>
        /// Construtor da classe de Usuários
        /// </summary>
        public Usuario(string nome)
            {
            Nome=nome;
            Senha=GetSenha(nome);
            Grupo=GetGrupo(nome);
            Nome_Completo=GetNomeCompleto(nome);
            }

        // Prorpiedades
        public string Nome { get; set; }

        public string Senha { get; private set; }
        public string Grupo { get; private set; }
        public string Nome_Completo { get; private set; }

        /// <summary>
        /// Metodo para obter a senha do usuário no banco de dados [Usuarios]
        /// </summary>
        private string GetSenha(string nome)
            {
            string comando = $"SELECT password FROM dbo.Usuarios WHERE usuario = '{nome}'";
            return FRS_Biblioteca.DB.ExecutaSQLparalista(comando)[0];
            }

        private string GetGrupo(string nome)
            {
            string comando = $"SELECT user_group FROM dbo.Usuarios WHERE usuario = '{nome}'";
            return FRS_Biblioteca.DB.ExecutaSQLparalista(comando)[0];
            }

        private string GetNomeCompleto(string nome)
            {
            string comando = $"SELECT Nome FROM dbo.Usuarios WHERE usuario = '{nome}'";
            return FRS_Biblioteca.DB.ExecutaSQLparalista(comando)[0];
            }
        }
    }