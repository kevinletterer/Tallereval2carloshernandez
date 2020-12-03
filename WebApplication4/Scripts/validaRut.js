function validaRut(rutIn) {
    //Debe ingresarse rut CON comillas
    rutA = rutIn;
    var split = rutA.toString().split("-");
    var digitov = split[1];
    var rut = split[0];
    if (digitov == 'K') digitov = 'k';
    return (verifd(rut) == digitov);
}

function verifd(K) {
    var i = 0, Suma = 1;
    for (; K; K = Math.floor(K / 10))
        Suma = (Suma + K % 10 * (9 - i++ % 6))
    bueno = Suma % 11;
    return bueno ? bueno - 1 : 'k';
}