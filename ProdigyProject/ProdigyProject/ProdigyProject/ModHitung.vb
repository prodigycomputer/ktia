Module ModHitung

    Public Function HitungJumlah(ByVal jlh1 As Decimal,
                              ByVal jlh2 As Decimal,
                              ByVal jlh3 As Decimal,
                              ByVal harga As Decimal,
                              ByVal disca As Decimal,
                              ByVal discb As Decimal,
                              ByVal discc As Decimal,
                              ByVal discrp As Decimal,
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
End Module
