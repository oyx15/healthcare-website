function Encrypt2Textbox(txt1, txt2) {
    EncryptTextbox(txt1);
    EncryptTextbox(txt2);
}

function Encrypt3Textbox(txt1, txt2, txt3) {
    EncryptTextbox(txt1);
    EncryptTextbox(txt2);
    EncryptTextbox(txt3);
}

function Encrypt6Textbox(txt1, txt2, txt3, txt4, txt5, txt6) {
    EncryptTextbox(txt1);
    EncryptTextbox(txt2);
    EncryptTextbox(txt3);
    EncryptTextbox(txt4);
    EncryptTextbox(txt5);
    EncryptTextbox(txt6);
}

function Decrypt3Textbox(txt1, txt2, txt3) {
    DecryptTextbox(txt1);
    DecryptTextbox(txt2);
    DecryptTextbox(txt3);    
}

function Decrypt4Textbox(txt1, txt2, txt3, txt4) {
    DecryptTextbox(txt1);
    DecryptTextbox(txt2);
    DecryptTextbox(txt3);
    DecryptTextbox(txt4);
}

function EncryptTextbox(ctrl) {
    var MyTextBox = document.getElementById(ctrl);
    var PlainText = MyTextBox.value;

    try {
        //Creating the Vector Key
        var iv = CryptoJS.enc.Hex.parse('a5s8d2e9c1721ae0e84ad660c472y1f3');
        //Encoding the Password in from UTF8 to byte array
        var Pass = CryptoJS.enc.Utf8.parse('Crypto');
        //Encoding the Salt in from UTF8 to byte array
        var Salt = CryptoJS.enc.Utf8.parse("cryptography123example");
        //Creating the key in PBKDF2 format to be used during the encryption
        var key128Bits1000Iterations = CryptoJS.PBKDF2(Pass.toString(CryptoJS.enc.Utf8), Salt, { keySize: 128 / 32, iterations: 1000 });

        //Encrypting the string contained in cipherParams using the PBKDF2 key
        var encrypted = CryptoJS.AES.encrypt(PlainText, key128Bits1000Iterations, { mode: CryptoJS.mode.CBC, iv: iv, padding: CryptoJS.pad.Pkcs7 });
        MyTextBox.value = encrypted.ciphertext.toString(CryptoJS.enc.Base64);

    }
    //Malformed UTF Data due to incorrect password
    catch (err) {
        return "";
    }

}

function DecryptTextbox(ctrl)
{            
    var MyTextBox = document.getElementById(ctrl);
    var encryptedData = MyTextBox.value;
    try
    {
        //Creating the Vector Key
        var iv = CryptoJS.enc.Hex.parse('a5s8d2e9c1721ae0e84ad660c472y1f3');
        //Encoding the Password in from UTF8 to byte array
        var Pass = CryptoJS.enc.Utf8.parse('Crypto');
        //Encoding the Salt in from UTF8 to byte array
        var Salt = CryptoJS.enc.Utf8.parse("cryptography123example");
        //Creating the key in PBKDF2 format to be used during the decryption
        var key128Bits1000Iterations = CryptoJS.PBKDF2(Pass.toString(CryptoJS.enc.Utf8), Salt, { keySize: 128 / 32, iterations: 1000 });
        //Enclosing the test to be decrypted in a CipherParams object as supported by the CryptoJS libarary
        var cipherParams = CryptoJS.lib.CipherParams.create({
            ciphertext: CryptoJS.enc.Base64.parse(encryptedData)
        });
 
        //Decrypting the string contained in cipherParams using the PBKDF2 key
        var decrypted = CryptoJS.AES.decrypt(cipherParams, key128Bits1000Iterations, { mode: CryptoJS.mode.CBC, iv: iv, padding: CryptoJS.pad.Pkcs7 });
        MyTextBox.value = decrypted.toString(CryptoJS.enc.Utf8);
    }
    //Malformed UTF Data due to incorrect password
    catch (err) {
        return "";
    }
}