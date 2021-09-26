export class Usuario {

    constructor(
        public usuarioId: string,
        public nome: string,
        public email: string,
        public senha: string,
        public funcao: string,
        public perfilId:string
    ) { }
    }