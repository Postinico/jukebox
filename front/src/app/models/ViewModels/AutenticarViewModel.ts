import { Usuario } from "../Usuario.Model";

export class AutenticarViewModel {

    constructor(
        public usuario: Usuario,
        public token: string
    ) { }
}