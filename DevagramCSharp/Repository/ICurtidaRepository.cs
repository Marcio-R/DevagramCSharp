﻿using DevagramCSharp.Models;

namespace DevagramCSharp.Repository
{
    public interface ICurtidaRepository
    {
        public void Curtir(Curtida curtida);
        public void Descurtir(Curtida descurtida);
        public Curtida GetCurtida(int IdPublicacao, int IdUsuario);
    }
}