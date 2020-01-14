module PfxUnpacking

open System.Security.Cryptography.X509Certificates

module X509Certificate2 =
    /// Export pfx private key as a base64 string
    let ExportPrivatePem(cert: X509Certificate2): string =
        cert.PrivateKey.ExportPkcs8PrivateKey() |> System.Convert.ToBase64String

    /// Export pfx public key as a base64 string
    let ExportPublicPem(cert: X509Certificate2): string = cert.Export(X509ContentType.Cert) |> System.Convert.ToBase64String

let printCert (pfxPath: string) (pfxPassphrase: string): unit =
    use cert = new X509Certificate2(pfxPath, pfxPassphrase, X509KeyStorageFlags.Exportable)

    let privatePem = X509Certificate2.ExportPrivatePem cert
    let publicPem = X509Certificate2.ExportPublicPem cert

    printfn "Private Key PEM:"
    printfn "%s" privatePem

    printfn "Public Key PEM:"
    printfn "%s" publicPem

[<EntryPoint>]
let main argv =
    if argv.Length < 1 then
        eprintfn "usage: <pfx path> [pfx passphrase]"
        1
    else
        let pfxPath = argv |> Array.item 0
        // pfx might not have a passphrase
        let pfxPassphrase =
            argv
            |> Array.tryItem 1
            |> Option.defaultValue ""

        printCert pfxPath pfxPassphrase
        0
