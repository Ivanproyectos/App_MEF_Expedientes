function MiRandom() {
    var crypto = window.crypto || window.msCrypto;
    var array = new Uint32Array(1);
    var nuevovalor = crypto.getRandomValues(array)[0];
    return nuevovalor;
}