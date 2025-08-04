-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 03 Agu 2025 pada 18.07
-- Versi server: 10.4.14-MariaDB
-- Versi PHP: 7.3.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbeiffelpetshop`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `barang`
--

CREATE TABLE `barang` (
  `kodebarang` varchar(12) NOT NULL,
  `namabarang` varchar(100) NOT NULL,
  `stok` int(3) NOT NULL,
  `satuan` varchar(5) NOT NULL,
  `jenisbarang` varchar(8) NOT NULL,
  `hargabeli` int(8) NOT NULL,
  `hargajual` int(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `barang`
--

INSERT INTO `barang` (`kodebarang`, `namabarang`, `stok`, `satuan`, `jenisbarang`, `hargabeli`, `hargajual`) VALUES
('860332079277', 'Obat Hijau ', 9, 'mL', 'Obat', 20000, 22000),
('874083469071', 'Ring Bite Shell', 47, 'Pcs', 'Mainan', 20000, 21000),
('882317076775', 'Makanan Kering', 18, 'Kg', 'Makanan', 200000, 210000),
('884442521573', 'Me-o kucing 220ml', 15, 'Lusin', 'Makanan', 6400, 7000),
('892712013871', 'Kandang Kucing Ukuran 80x80', 6, 'Pcs', 'Properti', 142000, 150000),
('893586310775', 'Serbuk Kasar', 49, 'Kg', 'Properti', 10000, 15000),
('898972513071', 'Kandang Hamster ', 19, 'Pcs', 'Properti', 65000, 70000);

-- --------------------------------------------------------

--
-- Struktur dari tabel `barangtransaksi`
--

CREATE TABLE `barangtransaksi` (
  `kodetransaksi` varchar(17) DEFAULT NULL,
  `kodebarang` varchar(12) DEFAULT NULL,
  `namabarang` varchar(100) DEFAULT NULL,
  `hargabarang` int(8) DEFAULT NULL,
  `jumlahbarang` int(3) DEFAULT NULL,
  `subtotal` int(8) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `barangtransaksi`
--

INSERT INTO `barangtransaksi` (`kodetransaksi`, `kodebarang`, `namabarang`, `hargabarang`, `jumlahbarang`, `subtotal`) VALUES
('EIF-020525-NP-433', '874083469071', 'Ring Bite Shell', 21000, 1, 21000),
('EIF-020525-NP-433', '882317076775', 'Makanan Kering', 210000, 1, 210000),
('EIF-020525-MP-500', '874083469071', 'Ring Bite Shell', 21000, 1, 21000),
('EIF-020525-MP-500', '884442521573', 'Me-o kucing 220ml', 7000, 1, 7000),
('EIF-020525-MP-297', '884442521573', 'Me-o kucing 220ml', 7000, 2, 14000),
('EIF-020525-MP-665', '892712013871', 'Kandang Kucing Ukuran 80x80', 150000, 1, 150000),
('EIF-050525-NP-933', '884442521573', 'Me-o kucing 220ml', 7000, 1, 7000),
('EIF-050525-NP-933', '892712013871', 'Kandang Kucing Ukuran 80x80', 150000, 1, 150000),
('EIF-050525-MP-896', '893586310775', 'Serbuk Kasar', 15000, 1, 15000),
('EIF-050525-MP-896', '898972513071', 'Kandang Hamster ', 70000, 1, 70000),
('EIF-050525-NP-653', '860332079277', 'Obat Hijau ', 22000, 1, 22000),
('EIF-050525-NP-653', '874083469071', 'Ring Bite Shell', 21000, 1, 21000),
('EIF-050525-MP-021', '882317076775', 'Makanan Kering', 210000, 1, 210000),
('EIF-050525-MP-021', '892712013871', 'Kandang Kucing Ukuran 80x80', 150000, 1, 150000),
('EIF-050525-MP-607', '884442521573', 'Me-o kucing 220ml', 7000, 1, 7000),
('EIF-050525-MP-607', '892712013871', 'Kandang Kucing Ukuran 80x80', 150000, 1, 150000);

-- --------------------------------------------------------

--
-- Struktur dari tabel `pelanggan`
--

CREATE TABLE `pelanggan` (
  `kodepelanggan` varchar(8) NOT NULL,
  `namapelanggan` varchar(100) NOT NULL,
  `alamat` varchar(150) NOT NULL,
  `nohandphone` varchar(13) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `pelanggan`
--

INSERT INTO `pelanggan` (`kodepelanggan`, `namapelanggan`, `alamat`, `nohandphone`) VALUES
('MP-41545', 'Lily Charissa Chang', 'Jalan Johar, gg lotus', '089632412415'),
('MP-468J0', 'Fitra Putra Pratama', 'Jl Ujung Pandang GG Lestari No.3C', '089524436468'),
('MP-56303', 'Muhammad Pedro Azani', 'Pal 5 Komp. Manday Lestari Permai No.31B', '085156515563'),
('MP-89807', 'Nicky Samuel', 'Adi Sucipto Komp.Parit Pantai', '082145677898'),
('NP-CASH', 'NonPelanggan', 'Eiffel Petshop', '000000000000');

-- --------------------------------------------------------

--
-- Struktur dari tabel `transaksi`
--

CREATE TABLE `transaksi` (
  `kodetransaksi` varchar(17) NOT NULL,
  `tanggal` date DEFAULT NULL,
  `Username` varchar(10) DEFAULT NULL,
  `tipepelanggan` varchar(13) DEFAULT NULL,
  `kodepelanggan` varchar(8) DEFAULT NULL,
  `namapelanggan` varchar(100) DEFAULT NULL,
  `jumlahbarang` int(8) DEFAULT NULL,
  `totalharga` int(8) DEFAULT NULL,
  `totalbayar` int(8) DEFAULT NULL,
  `dibayar` int(8) DEFAULT NULL,
  `kembalian` int(8) DEFAULT NULL,
  `UserID` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `transaksi`
--

INSERT INTO `transaksi` (`kodetransaksi`, `tanggal`, `Username`, `tipepelanggan`, `kodepelanggan`, `namapelanggan`, `jumlahbarang`, `totalharga`, `totalbayar`, `dibayar`, `kembalian`, `UserID`) VALUES
('EIF-020525-MP-297', '2025-05-02', 'Pedro', 'Pelanggan', 'MP-56303', 'Muhammad Pedro Azani', 2, 14000, 14000, 20000, 6000, 'UR001-BB35'),
('EIF-020525-MP-500', '2025-05-02', 'Pedro', 'Pelanggan', 'MP-89807', 'Nicky Samuel', 2, 28000, 28000, 50000, 22000, 'UR001-BB35'),
('EIF-020525-MP-665', '2025-05-02', 'Pedro', 'Pelanggan', 'MP-41545', 'Lily Charissa Chang', 1, 150000, 150000, 150000, 0, 'UR001-BB35'),
('EIF-020525-NP-433', '2025-05-02', 'Pedro', 'Non-Pelanggan', 'NP-CASH', 'NonPelanggan', 2, 231000, 231000, 250000, 19000, 'UR001-BB35'),
('EIF-050525-MP-021', '2025-05-05', 'Pedro', 'Pelanggan', 'MP-41545', 'Lily Charissa Chang', 2, 360000, 360000, 400000, 40000, 'UR001-BB35'),
('EIF-050525-MP-607', '2025-05-05', 'Pedro', 'Pelanggan', 'MP-89807', 'Nicky Samuel', 2, 157000, 157000, 160000, 3000, 'UR001-BB35'),
('EIF-050525-MP-896', '2025-05-05', 'Pedro', 'Pelanggan', 'MP-56303', 'Muhammad Pedro Azani', 2, 85000, 85000, 100000, 15000, 'UR001-BB35'),
('EIF-050525-NP-653', '2025-05-05', 'Pedro', 'Non-Pelanggan', 'NP-CASH', 'NonPelanggan', 2, 43000, 43000, 50000, 7000, 'UR001-BB35'),
('EIF-050525-NP-933', '2025-05-05', 'Pedro', 'Non-Pelanggan', 'NP-CASH', 'NonPelanggan', 2, 157000, 157000, 200000, 43000, 'UR001-BB35');

-- --------------------------------------------------------

--
-- Struktur dari tabel `users`
--

CREATE TABLE `users` (
  `UserID` varchar(10) NOT NULL,
  `Username` varchar(40) NOT NULL,
  `Password` varchar(20) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `NoHp` varchar(13) DEFAULT NULL,
  `Alamat` varchar(150) DEFAULT NULL,
  `Posisi` varchar(8) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `users`
--

INSERT INTO `users` (`UserID`, `Username`, `Password`, `Email`, `NoHp`, `Alamat`, `Posisi`) VALUES
('UR001-BB35', 'Pedro', '123456789', 'pedroazani08@gmail.com', '089524436468', 'Komp.Manday Lestari Permai No 31.B', 'Pimpinan');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `barang`
--
ALTER TABLE `barang`
  ADD PRIMARY KEY (`kodebarang`);

--
-- Indeks untuk tabel `barangtransaksi`
--
ALTER TABLE `barangtransaksi`
  ADD KEY `kodetransaksi` (`kodetransaksi`),
  ADD KEY `kodebarang` (`kodebarang`);

--
-- Indeks untuk tabel `pelanggan`
--
ALTER TABLE `pelanggan`
  ADD PRIMARY KEY (`kodepelanggan`);

--
-- Indeks untuk tabel `transaksi`
--
ALTER TABLE `transaksi`
  ADD PRIMARY KEY (`kodetransaksi`),
  ADD KEY `kodepelanggan` (`kodepelanggan`),
  ADD KEY `UserID` (`UserID`);

--
-- Indeks untuk tabel `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserID`);

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `barangtransaksi`
--
ALTER TABLE `barangtransaksi`
  ADD CONSTRAINT `barangtransaksi_ibfk_1` FOREIGN KEY (`kodetransaksi`) REFERENCES `transaksi` (`kodetransaksi`),
  ADD CONSTRAINT `barangtransaksi_ibfk_2` FOREIGN KEY (`kodebarang`) REFERENCES `barang` (`kodebarang`);

--
-- Ketidakleluasaan untuk tabel `transaksi`
--
ALTER TABLE `transaksi`
  ADD CONSTRAINT `transaksi_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`),
  ADD CONSTRAINT `transaksi_ibfk_2` FOREIGN KEY (`kodepelanggan`) REFERENCES `pelanggan` (`kodepelanggan`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
