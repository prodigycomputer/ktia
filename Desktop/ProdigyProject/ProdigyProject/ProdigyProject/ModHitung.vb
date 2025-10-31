Module ModHitung

    Public Function HitungJumlah(Optional ByVal jlh1 As Decimal = 0,
                              Optional ByVal jlh2 As Decimal = 0,
                              Optional ByVal jlh3 As Decimal = 0,
                              Optional ByVal harga As Decimal = 0,
                              Optional ByVal disca As Decimal = 0,
                              Optional ByVal discb As Decimal = 0,
                              Optional ByVal discc As Decimal = 0,
                              Optional ByVal discrp As Decimal = 0,
                              Optional ByVal isi1 As Decimal = 1,
                              Optional ByVal isi2 As Decimal = 1) As Dictionary(Of String, Decimal)

        ' harga untuk satuan 1
        Dim hasil1 As Decimal = jlh1 * harga
        ' harga untuk satuan 2
        Dim hasil2 As Decimal = If(jlh2 > 0, (harga / isi1) * jlh2, 0)
        ' harga untuk satuan 3
        Dim hasil3 As Decimal = If(jlh3 > 0, (harga / (isi1 * isi2)) * jlh3, 0)

        Dim subtotal As Decimal = hasil1 + hasil2 + hasil3

        Dim afterDisca As Decimal = subtotal * disca / 100
        Dim smntaraDis1 As Decimal = subtotal - afterDisca

        Dim afterDiscb As Decimal = smntaraDis1 * discb / 100
        Dim smntaraDis2 As Decimal = smntaraDis1 - afterDiscb

        Dim afterDiscc As Decimal = smntaraDis2 * discc / 100
        Dim smntaraDis3 As Decimal = smntaraDis2 - afterDiscc

        Dim finalJumlah As Decimal = smntaraDis3 - discrp

        Dim hasil As New Dictionary(Of String, Decimal)
        hasil("subtotal") = subtotal
        hasil("hdisca") = afterDisca
        hasil("hdiscb") = afterDiscb
        hasil("hdiscc") = afterDiscc
        hasil("final") = Math.Round(finalJumlah)

        Return hasil
    End Function

    Public Function HitungSubtotalTotal(Optional ByVal subtotal As Decimal = 0,
                                    Optional ByVal disc1 As Decimal = 0,
                                    Optional ByVal disc2 As Decimal = 0,
                                    Optional ByVal disc3 As Decimal = 0,
                                    Optional ByVal ppn As Decimal = 0,
                                    Optional ByVal lainLain As Decimal = 0) As Dictionary(Of String, Decimal)

        ' diskon 1
        Dim hrgdc1 As Decimal = subtotal * disc1 / 100
        Dim smntarahrgdc1 As Decimal = subtotal - hrgdc1

        ' diskon 2
        Dim hrgdc2 As Decimal = smntarahrgdc1 * disc2 / 100
        Dim smntarahrgdc2 As Decimal = smntarahrgdc1 - hrgdc2

        ' diskon 3
        Dim hrgdc3 As Decimal = smntarahrgdc2 * disc3 / 100
        Dim smntarahrgdc3 As Decimal = smntarahrgdc2 - hrgdc3

        ' ppn
        Dim hrppn As Decimal = smntarahrgdc3 * ppn / 100
        Dim totalppn As Decimal = smntarahrgdc3 + hrppn

        ' total akhir + lain-lain
        Dim totaljmlh As Decimal = totalppn + lainLain

        Dim hasil As New Dictionary(Of String, Decimal)
        hasil("subtotal") = subtotal
        hasil("disc1") = disc1
        hasil("disc2") = disc2
        hasil("disc3") = disc3
        hasil("hdisca") = hrgdc1
        hasil("hdiscb") = hrgdc2
        hasil("hdiscc") = hrgdc3
        hasil("lain") = lainLain
        hasil("ppn") = hrppn
        hasil("totalppn") = totalppn
        hasil("total") = Math.Round(totaljmlh)

        Return hasil
    End Function

End Module
