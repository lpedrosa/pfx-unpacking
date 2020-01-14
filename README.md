# PfxUnpacking

Simple .NET Core 3 example showing how to read a PKCS12/PFX file and extracting the private and public keys in the PEM format.

## Usage

Change to the project directory and run:

```
dotnet run <pfx-path> [pfx-passphrase]
```

The pfx passphrase is optional, as the pfx might not have a passphrase.

This should print something like this:

```
Private Key PEM:
<base64 string of the private key...>
Public Key PEM:
<base64 string of the public key...>
```

## Rationale

Often when dealing with certificates (e.g. signing an HTTP request), you need to send the public key of the certificate, in PEM format. This example shows how you can extract the public key in the PEM format, as well as the private key.

I did a lot of digging around, but it was not clear how to obtain the PEM format of the keys. This [blog](http://paulstovell.com/blog/x509certificate2) certainly helped.

Another thing that worth mentioning is that you have instantiate the certificate as exportable, if you wish to export the private key of the certificate.

```
var cert = new X509Certificate("pfx-path", "pfx-passphrase", X509KeyStorageFlags.Exportable)
// this line will throw if you don't set the proper flag
cert.PrivateKey.ExportPkcs8PrivateKey()
```

On Linux, I believe the private key is exportable by default (missing source). That is not the case on Mac and Windows.

