<!DOCTYPE html>
<html>
<head>
  <meta charset="UTF-8">
  <title>Preview Nota</title>
  <style>
    html, body {
      margin: 0; padding: 0;
      font-family: Verdana;
      font-size: 12px;
      background: #eee;
    }
    .container-a5 {
      width: 210mm;
      height: 148mm; /* A5 landscape tinggi lebih pas 148mm */
      background: #fff;
      margin: 20px auto;
      padding: 5mm;
      box-shadow: 0 0 5px rgba(0,0,0,0.2);
      box-sizing: border-box;
    }
    h2 { margin: 0 0 10px; }

    .form-header {
      display: flex;
      justify-content: space-between;
      margin-bottom: 5px;
    }
    .form-kiri, .form-kanan { font-size: 12px; }
    .form-kiri { width: 35%; }
    .form-kanan { width: 60%; }

    .field { display: flex; }
    .field label:first-child { width: 100px; }
    .field label:nth-child(2) { margin: 0 4px; }

    table { border-collapse: collapse; width: 100%; margin-top: 5px; }
    th, td {
      border: 1px solid #000;
      padding: 3px;
      font-size: 12px;
      white-space: nowrap;
      overflow: hidden;
      text-overflow: ellipsis;
    }
    th { text-align: center; background: #f5f5f5; }
    td { text-align: left; }

    .form-footer {
      display: flex;
      justify-content: space-between;
      margin-top: 10px;
    }

    .footer-kiri {
      width: 55%;
      text-align: center;
      font-size: 12px;
    }
    .box-perhatian {
      border: 1px solid;
      padding: 10px;
      margin-bottom: 10px;
      font-style: italic;
    }
    .footer-ttd-row { display: flex; justify-content: space-between; }
    .kolom-ttd { width: 45%; }

    .footer-kanan {
      width: 40%;
      font-size: 12px;
    }
    .footer-kanan div {
      display: flex;
      justify-content: space-between;
      margin-bottom: 2px;
    }

    @page { size: A5 landscape; margin: 0; }
    @media print {
      body { background: none; margin: 0; }
      .container-a5 { margin: 0; box-shadow: none; }
    }
  </style>
</head>
<body>
  <div class="container-a5">
    <h2>NOTA PEMBELIAN</h2>
    <div class="form-header">
      <div class="form-kiri">
        <div class="field"><label>Tanggal</label><label>:</label><span>13-09-2025</span></div>
        <div class="field"><label>No Nota</label><label>:</label><span>NB-001</span></div>
        <div class="field"><label>Jatuh Tempo</label><label>:</label><span>20-09-2025</span></div>
      </div>
      <div class="form-kanan">
        <div class="field"><label>Kode Supplier</label><label>:</label><span>SUP001</span></div>
        <div class="field"><label>Nama Supplier</label><label>:</label><span>PT Supplier Jaya</span></div>
        <div class="field"><label>Alamat</label><label>:</label><span>Jl. Merdeka No. 123</span></div>
      </div>
    </div>

    <table>
      <thead>
        <tr>
          <th style="width:60px">Kode</th>
          <th style="width:150px">Nama Brg</th>
          <th style="width:60px">QTY</th>
          <th style="width:50px">Disc %</th>
          <th style="width:60px">Dis Rp</th>
          <th style="width:80px">Harga</th>
          <th style="width:80px">Jumlah</th>
        </tr>
      </thead>
      <tbody>
        <tr><td>BRG001</td><td>Kabel UTP Cat6</td><td align="right">10</td><td align="right">5</td><td align="right">0</td><td align="right">5000</td><td align="right">47,500</td></tr>
        <tr><td>BRG002</td><td>Keyboard Wireless</td><td align="right">2</td><td align="right">0</td><td align="right">0</td><td align="right">120,000</td><td align="right">240,000</td></tr>
        <tr><td>BRG003</td><td>Mouse Wireless</td><td align="right">2</td><td align="right">0</td><td align="right">0</td><td align="right">80,000</td><td align="right">160,000</td></tr>
        <tr><td>BRG004</td><td>Printer Epson L3110</td><td align="right">1</td><td align="right">2</td><td align="right">10,000</td><td align="right">2,000,000</td><td align="right">1,960,000</td></tr>
        <tr><td>BRG005</td><td>Paper Roll 75x65</td><td align="right">12</td><td align="right">0</td><td align="right">0</td><td align="right">5,000</td><td align="right">60,000</td></tr>
      </tbody>
    </table>

    <div class="form-footer">
      <div class="footer-kiri">
        <div class="box-perhatian">Barang yang sudah dibeli tidak bisa ditukar/dikembalikan</div>
        <div class="footer-ttd-row">
          <div class="kolom-ttd">
            <strong>Tanda Terima</strong><br><br><br>(___________)
          </div>
          <div class="kolom-ttd">
            <strong>Hormat Kami</strong><br><br><br>(___________)
          </div>
        </div>
      </div>
      <div class="footer-kanan">
        <div><label>Subtotal :</label><span>2,467,500</span></div>
        <div><label>Diskon 1 :</label><span>5%</span></div>
        <div><label>Lain-lain :</label><span>0</span></div>
        <div><label>PPN :</label><span>11%</span></div>
        <div style="border-top:1px solid #000;margin-top:4px;padding-top:4px;">
          <label><strong>Total :</strong></label><span><strong>2,739,000</strong></span>
        </div>
      </div>
    </div>
  </div>
</body>
</html>
