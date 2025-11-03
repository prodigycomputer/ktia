-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 03, 2025 at 11:10 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbkita`
--

-- --------------------------------------------------------

--
-- Table structure for table `zakses`
--

CREATE TABLE `zakses` (
  `kodeuser` char(11) NOT NULL,
  `idmenu` varchar(10) NOT NULL,
  `tambah` int(1) NOT NULL DEFAULT 1,
  `ubah` int(1) DEFAULT 1,
  `hapus` int(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zakses`
--

INSERT INTO `zakses` (`kodeuser`, `idmenu`, `tambah`, `ubah`, `hapus`) VALUES
('USER01', 'ME001', 1, 0, 0),
('USER01', 'ME002', 1, 1, 1),
('USER01', 'ME003', 1, 1, 1),
('USER01', 'ME004', 1, 1, 1),
('USER01', 'ME005', 1, 1, 1),
('USER01', 'ME006', 1, 1, 1),
('USER01', 'ME0020', 1, 1, 1),
('USER01', 'ME007', 1, 1, 1),
('USER01', 'ME008', 1, 1, 1),
('USER01', 'ME009', 1, 1, 1),
('USER01', 'ME0010', 1, 1, 1),
('USER01', 'ME0011', 1, 1, 1),
('USER01', 'ME0012', 1, 1, 1),
('USER01', 'ME0019', 1, 1, 1),
('USER01', 'ME0021', 1, 1, 1),
('USER01', 'ME0013', 1, 1, 1),
('USER01', 'ME0014', 1, 0, 0),
('USER01', 'ME0015', 1, 1, 1),
('USER01', 'ME0016', 1, 0, 0),
('USER01', 'ME0017', 1, 1, 1),
('USER01', 'ME0018', 1, 1, 1),
('USER00', 'ME001', 1, 1, 1),
('USER00', 'ME002', 1, 1, 1),
('USER00', 'ME003', 1, 1, 1),
('USER00', 'ME004', 1, 1, 1),
('USER00', 'ME005', 1, 1, 1),
('USER00', 'ME006', 1, 1, 1),
('USER00', 'ME0020', 1, 1, 1),
('USER00', 'ME007', 1, 1, 1),
('USER00', 'ME008', 1, 1, 1),
('USER00', 'ME009', 1, 1, 1),
('USER00', 'ME0010', 1, 1, 1),
('USER00', 'ME0011', 1, 1, 1),
('USER00', 'ME0012', 1, 1, 1),
('USER00', 'ME0019', 1, 1, 1),
('USER00', 'ME0021', 1, 1, 1),
('USER00', 'ME0022', 1, 1, 1),
('USER00', 'ME0023', 1, 1, 1),
('USER00', 'ME0013', 1, 1, 1),
('USER00', 'ME0014', 1, 1, 1),
('USER00', 'ME0015', 1, 1, 1),
('USER00', 'ME0016', 1, 1, 1),
('USER00', 'ME0017', 1, 1, 1),
('USER00', 'ME0018', 1, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `zarea`
--

CREATE TABLE `zarea` (
  `kodear` char(20) NOT NULL,
  `namaar` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zarea`
--

INSERT INTO `zarea` (`kodear`, `namaar`) VALUES
('AR01', 'SUNGAI JAWI'),
('AR02', 'SUNGAI BANGKONG');

-- --------------------------------------------------------

--
-- Table structure for table `zbeli`
--

CREATE TABLE `zbeli` (
  `nonota` char(30) DEFAULT NULL,
  `tgl` date DEFAULT NULL,
  `kodesup` char(10) DEFAULT NULL,
  `kodegd` char(10) NOT NULL,
  `nilai` double DEFAULT 0,
  `lunas` int(11) DEFAULT 0,
  `tgltempo` date DEFAULT NULL,
  `ppn` double(10,2) DEFAULT 0.00,
  `hppn` double(10,2) DEFAULT 0.00,
  `disc1` double(10,2) DEFAULT 0.00,
  `hdisc1` double(10,2) DEFAULT 0.00,
  `disc2` double(10,2) DEFAULT 0.00,
  `hdisc2` double(10,2) DEFAULT 0.00,
  `disc3` double(10,2) DEFAULT 0.00,
  `hdisc3` double(10,2) DEFAULT 0.00,
  `ket` varchar(1000) DEFAULT NULL,
  `operator` char(10) DEFAULT '',
  `logtime` timestamp NOT NULL DEFAULT current_timestamp(),
  `lainnya` double(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zbeli`
--

INSERT INTO `zbeli` (`nonota`, `tgl`, `kodesup`, `kodegd`, `nilai`, `lunas`, `tgltempo`, `ppn`, `hppn`, `disc1`, `hdisc1`, `disc2`, `hdisc2`, `disc3`, `hdisc3`, `ket`, `operator`, `logtime`, `lainnya`) VALUES
('NOTA3', '2025-09-25', 'S002', '', 1678302, 0, '2025-09-25', 11.00, 163345.27, 10.00, 203697.80, 10.00, 183328.02, 10.00, 164995.22, 'TEST NOTA 3', '', '2025-10-03 04:12:56', 30000.00),
('NOTA1', '2025-09-25', 'S001', '', 3134376, 0, '2025-09-25', 11.00, 310415.65, 10.00, 387100.20, 10.00, 348390.18, 10.00, 313551.16, 'TEST NOTA 1', '', '2025-10-04 01:46:21', 2000.00),
('NOTA2', '2025-10-02', 'S002', '', 2604289, 0, '2025-10-31', 11.00, 257091.71, 10.00, 360678.60, 10.00, 324610.74, 20.00, 584299.33, 'TEST NOTA 2', '', '2025-10-21 06:08:17', 10000.00),
('NOTA4', '2025-10-21', 'S002', '', 24375700, 0, '2025-10-21', 11.00, 2405700.00, 10.00, 3000000.00, 10.00, 2700000.00, 10.00, 2430000.00, 'TEST', '', '2025-10-21 06:23:59', 100000.00);

-- --------------------------------------------------------

--
-- Table structure for table `zbelim`
--

CREATE TABLE `zbelim` (
  `nonota` char(30) DEFAULT NULL,
  `kodebrg` char(20) DEFAULT NULL,
  `kodegd` char(10) NOT NULL,
  `jlh1` double DEFAULT 0,
  `jlh2` double DEFAULT 0,
  `jlh3` double DEFAULT 0,
  `harga` double DEFAULT 9,
  `disca` double DEFAULT 0,
  `hdisca` double DEFAULT 0,
  `discb` double DEFAULT 0,
  `hdiscb` double DEFAULT 0,
  `discc` double DEFAULT 0,
  `hdiscc` double DEFAULT 0,
  `discrp` double DEFAULT 0,
  `jumlah` double DEFAULT 0,
  `logtime` timestamp NOT NULL DEFAULT current_timestamp(),
  `operator` char(10) DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zbelim`
--

INSERT INTO `zbelim` (`nonota`, `kodebrg`, `kodegd`, `jlh1`, `jlh2`, `jlh3`, `harga`, `disca`, `hdisca`, `discb`, `hdiscb`, `discc`, `hdiscc`, `discrp`, `jumlah`, `logtime`, `operator`) VALUES
('NOTA3', 'ALB0001', 'G02', 1, 0, 0, 3000000, 10, 0, 10, 0, 10, 0, 1000000, 1187000, '2025-10-03 04:12:56', 'User'),
('NOTA3', 'BAT0002', 'G02', 1, 3, 0, 225000, 20, 0, 0, 0, 0, 0, 0, 234000, '2025-10-03 04:12:56', 'User'),
('NOTA3', 'CAR0035', 'G03', 1, 2, 3, 30000, 10, 0, 10, 0, 0, 0, 0, 35478, '2025-10-03 04:12:56', 'User'),
('NOTA3', 'KEY0004', 'G01', 1, 2, 3, 300000, 10, 0, 0, 0, 0, 0, 0, 580500, '2025-10-03 04:12:56', 'User'),
('NOTA1', 'ALB0001', 'G02', 1, 0, 0, 3000000, 10, 0, 10, 0, 10, 0, 1000000, 1187000, '2025-10-04 01:46:21', 'User'),
('NOTA1', 'BAT0002', 'G02', 1, 3, 0, 225000, 20, 0, 0, 0, 0, 0, 0, 234000, '2025-10-04 01:46:21', 'User'),
('NOTA1', 'CAR0079', 'G02', 1, 2, 3, 50000, 0, 0, 0, 0, 0, 0, 0, 60750, '2025-10-04 01:46:21', 'User'),
('NOTA1', 'CAR0035', 'G03', 1, 2, 3, 30000, 10, 0, 10, 0, 0, 0, 0, 35478, '2025-10-04 01:46:21', 'User'),
('NOTA1', 'CAR0043', 'G03', 1, 0, 0, 250000, 0, 0, 0, 0, 0, 0, 0, 250000, '2025-10-04 01:46:21', 'User'),
('NOTA1', 'KAB0064', 'G02', 1, 2, 0, 20000, 10, 0, 10, 0, 0, 0, 0, 16524, '2025-10-04 01:46:21', 'User'),
('NOTA1', 'KAB0064', 'G02', 1, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 20000, '2025-10-04 01:46:21', 'User'),
('NOTA1', 'KEY0004', 'G01', 1, 2, 3, 300000, 10, 0, 0, 0, 0, 0, 0, 580500, '2025-10-04 01:46:21', 'User'),
('NOTA1', 'PAP0008', 'G01', 2, 0, 0, 110000, 0, 0, 0, 0, 0, 0, 0, 220000, '2025-10-04 01:46:21', 'User'),
('NOTA1', 'PAP0010', 'G03', 10, 0, 0, 3400, 0, 0, 0, 0, 0, 0, 0, 34000, '2025-10-04 01:46:21', 'User'),
('NOTA1', 'HDD0048', 'G01', 1, 0, 0, 1100000, 0, 0, 0, 0, 0, 0, 0, 1100000, '2025-10-04 01:46:21', 'User'),
('NOTA1', 'TIN0081', 'G03', 1, 1, 0, 60000, 0, 0, 0, 0, 0, 0, 0, 72000, '2025-10-04 01:46:21', 'User'),
('NOTA1', 'CAR0079', 'G02', 1, 2, 3, 50000, 0, 0, 0, 0, 0, 0, 0, 60750, '2025-10-04 01:46:21', 'User'),
('NOTA 4', 'KEY0004', 'G02', 12, 12, 12, 300000, 11, 0, 10, 0, 10, 0, 10000, 4012622, '2025-10-21 01:43:14', 'User'),
('NOTA2', 'ALB0001', 'G02', 1, 0, 0, 3000000, 10, 0, 0, 0, 0, 0, 0, 2700000, '2025-10-21 06:08:17', 'User'),
('NOTA2', 'BAT0002', 'G02', 1, 3, 0, 225000, 20, 0, 0, 0, 0, 0, 0, 234000, '2025-10-21 06:08:17', 'User'),
('NOTA2', 'CAR0079', 'G02', 1, 2, 3, 50000, 0, 0, 0, 0, 0, 0, 0, 60750, '2025-10-21 06:08:17', 'User'),
('NOTA2', 'CAR0035', 'G01', 1, 2, 3, 30000, 20, 0, 10, 0, 0, 0, 0, 31536, '2025-10-21 06:08:17', 'User'),
('NOTA2', 'KEY0004', 'G01', 1, 2, 3, 300000, 10, 0, 0, 0, 0, 0, 0, 580500, '2025-10-21 06:08:17', 'User'),
('NOTA4', 'ALB0001', 'G01', 10, 0, 0, 3000000, 0, 0, 0, 0, 0, 0, 0, 30000000, '2025-10-21 06:23:59', 'User');

-- --------------------------------------------------------

--
-- Table structure for table `zconfig`
--

CREATE TABLE `zconfig` (
  `jmlharga` int(11) DEFAULT 12,
  `qppn` double(10,2) DEFAULT 11.00,
  `qbrsbeli` int(11) NOT NULL,
  `qbrsjual` int(11) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `zconfig`
--

INSERT INTO `zconfig` (`jmlharga`, `qppn`, `qbrsbeli`, `qbrsjual`) VALUES
(6, 11.00, 10, 10);

-- --------------------------------------------------------

--
-- Table structure for table `zgolongan`
--

CREATE TABLE `zgolongan` (
  `kodegol` char(20) NOT NULL,
  `namagol` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zgolongan`
--

INSERT INTO `zgolongan` (`kodegol`, `namagol`) VALUES
('GOL01', 'GOLONGAN 1'),
('GOL02', 'GOLONGAN 2');

-- --------------------------------------------------------

--
-- Table structure for table `zgrup`
--

CREATE TABLE `zgrup` (
  `kodegrup` char(20) NOT NULL,
  `namagrup` varchar(50) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `zgrup`
--

INSERT INTO `zgrup` (`kodegrup`, `namagrup`) VALUES
('A01', 'ROKOK'),
('A02', 'SUSU'),
('A03', 'LAIN - LAIN');

-- --------------------------------------------------------

--
-- Table structure for table `zgudang`
--

CREATE TABLE `zgudang` (
  `kodegd` char(20) NOT NULL,
  `namagd` char(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zgudang`
--

INSERT INTO `zgudang` (`kodegd`, `namagd`) VALUES
('G01', 'GUDANG 01'),
('G02', 'GUDANG 02'),
('G03', 'GUDANG 03');

-- --------------------------------------------------------

--
-- Table structure for table `zjual`
--

CREATE TABLE `zjual` (
  `nonota` char(30) DEFAULT NULL,
  `tgl` date DEFAULT NULL,
  `kodekust` char(10) DEFAULT NULL,
  `kodesls` char(10) DEFAULT NULL,
  `kodegd` char(10) DEFAULT NULL,
  `nilai` double DEFAULT 0,
  `lunas` int(11) DEFAULT 0,
  `tgltempo` date DEFAULT NULL,
  `ppn` double(10,2) DEFAULT 0.00,
  `hppn` double(10,2) DEFAULT 0.00,
  `disc1` double(10,2) DEFAULT 0.00,
  `hdisc1` double(10,2) DEFAULT 0.00,
  `disc2` double(10,2) DEFAULT 0.00,
  `hdisc2` double(10,2) DEFAULT 0.00,
  `disc3` double(10,2) DEFAULT 0.00,
  `hdisc3` double(10,2) DEFAULT 0.00,
  `ket` varchar(1000) DEFAULT NULL,
  `operator` char(10) DEFAULT '',
  `logtime` timestamp NOT NULL DEFAULT current_timestamp(),
  `lainnya` double(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zjual`
--

INSERT INTO `zjual` (`nonota`, `tgl`, `kodekust`, `kodesls`, `kodegd`, `nilai`, `lunas`, `tgltempo`, `ppn`, `hppn`, `disc1`, `hdisc1`, `disc2`, `hdisc2`, `disc3`, `hdisc3`, `ket`, `operator`, `logtime`, `lainnya`) VALUES
('NOTA1', '2025-10-07', 'C01', 'S01', NULL, 27746, 0, '2025-10-07', 11.00, 1758.57, 10.00, 2193.00, 10.00, 1973.70, 10.00, 1776.33, 'TEST', '', '2025-10-21 02:03:47', 10000.00);

-- --------------------------------------------------------

--
-- Table structure for table `zjualm`
--

CREATE TABLE `zjualm` (
  `nonota` char(30) DEFAULT NULL,
  `kodebrg` char(20) DEFAULT NULL,
  `kodegd` char(10) DEFAULT NULL,
  `jlh1` double DEFAULT 0,
  `jlh2` double DEFAULT 0,
  `jlh3` double DEFAULT 0,
  `harga` double DEFAULT 9,
  `disca` double DEFAULT 0,
  `hdisca` double DEFAULT 0,
  `discb` double DEFAULT 0,
  `hdiscb` double DEFAULT 0,
  `discc` double DEFAULT 0,
  `hdiscc` double DEFAULT 0,
  `discrp` double DEFAULT 0,
  `jumlah` double DEFAULT 0,
  `operator` char(10) DEFAULT '',
  `logtime` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zjualm`
--

INSERT INTO `zjualm` (`nonota`, `kodebrg`, `kodegd`, `jlh1`, `jlh2`, `jlh3`, `harga`, `disca`, `hdisca`, `discb`, `hdiscb`, `discc`, `hdiscc`, `discrp`, `jumlah`, `operator`, `logtime`) VALUES
('NOTA1', 'CAR0035', 'G01', 1, 2, 3, 30000, 10, 0, 10, 0, 10, 0, 10000, 21930, 'User', '2025-10-21 02:03:47');

-- --------------------------------------------------------

--
-- Table structure for table `zkolektor`
--

CREATE TABLE `zkolektor` (
  `kodekol` char(20) NOT NULL,
  `namakol` char(50) NOT NULL,
  `alamat` char(255) NOT NULL,
  `kota` char(50) NOT NULL,
  `ktp` varchar(20) NOT NULL,
  `npwp` varchar(30) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `zkolektor`
--

INSERT INTO `zkolektor` (`kodekol`, `namakol`, `alamat`, `kota`, `ktp`, `npwp`) VALUES
('KOL01', 'KOLEKTOR 1', 'JL JAWI', 'PONTIANAK', 'TEST', 'TEST');

-- --------------------------------------------------------

--
-- Table structure for table `zkustomer`
--

CREATE TABLE `zkustomer` (
  `kodekust` char(10) NOT NULL,
  `kodear` char(20) DEFAULT NULL,
  `kodetipe` char(20) DEFAULT NULL,
  `namakust` tinytext NOT NULL,
  `alamat` tinytext NOT NULL,
  `kota` tinytext DEFAULT NULL,
  `kodehrg` char(2) NOT NULL,
  `ktp` char(20) NOT NULL,
  `npwp` char(30) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `zkustomer`
--

INSERT INTO `zkustomer` (`kodekust`, `kodear`, `kodetipe`, `namakust`, `alamat`, `kota`, `kodehrg`, `ktp`, `npwp`) VALUES
('C01', 'AR01', 'TP02', 'PT MEDIA INDAH', 'JL. MENTARI', 'PONTIANAK', '2', '3201021501010001', '12.345.678.9-012.345'),
('C02', 'AR02', 'TP01', 'PT ASIA JAYA ABADI', 'JL. SITAPANG', 'SINGKAWANG', '5', '3201021501010002', '12.345.678.9-013.345');

-- --------------------------------------------------------

--
-- Table structure for table `zmenu`
--

CREATE TABLE `zmenu` (
  `idmenu` char(20) NOT NULL,
  `mainmenu` varchar(50) NOT NULL,
  `submenu` varchar(50) NOT NULL,
  `urutan` int(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zmenu`
--

INSERT INTO `zmenu` (`idmenu`, `mainmenu`, `submenu`, `urutan`) VALUES
('ME001', 'mFile', 'smStok', 100),
('ME002', 'mFile', 'smArea', 200),
('ME003', 'mFile', 'smTipe', 300),
('ME004', 'mFile', 'smKustomer', 400),
('ME005', 'mFile', 'smSupplier', 500),
('ME006', 'mFile', 'smSales', 600),
('ME0020', 'mFile', 'smKolektor', 601),
('ME007', 'mFile', 'smGudang', 700),
('ME008', 'mFile', 'smMerek', 800),
('ME009', 'mFile', 'smGolongan', 900),
('ME0010', 'mFile', 'smGrup', 1000),
('ME0011', 'mFile', 'smAkun', 1100),
('ME0012', 'mFile', 'smExit', 1200),
('ME0013', 'mTransaksi', 'smPembelian', 1300),
('ME0014', 'mTransaksi', 'smReturPembelian', 1400),
('ME0015', 'mTransaksi', 'smPenjualan', 1500),
('ME0016', 'mTransaksi', 'smReturPenjualan', 1600),
('ME0017', 'mTransaksi', 'smMutasi', 1700),
('ME0018', 'mTransaksi', 'smPenyesuaian', 1800),
('ME0019', 'mLaporan', 'smLaporanPembelian', 1900),
('ME0021', 'mLaporan', 'smLaporanPenjualan', 1901),
('ME0022', 'mLaporan', 'smLaporanMutasi', 1902),
('ME0023', 'mLaporan', 'smLaporanPenyesuaian', 1903);

-- --------------------------------------------------------

--
-- Table structure for table `zmerek`
--

CREATE TABLE `zmerek` (
  `kodemerk` char(20) NOT NULL,
  `namamerk` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zmerek`
--

INSERT INTO `zmerek` (`kodemerk`, `namamerk`) VALUES
('M01', 'INDOMIE');

-- --------------------------------------------------------

--
-- Table structure for table `zmutasi`
--

CREATE TABLE `zmutasi` (
  `nonota` char(30) NOT NULL,
  `tgl` date DEFAULT NULL,
  `kodegd1` char(10) DEFAULT NULL,
  `kodegd2` char(10) NOT NULL,
  `operator` char(10) DEFAULT '',
  `logtime` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zmutasi`
--

INSERT INTO `zmutasi` (`nonota`, `tgl`, `kodegd1`, `kodegd2`, `operator`, `logtime`) VALUES
('NOTA1', '2025-10-09', 'G02', 'G01', '', '2025-10-09 08:07:07'),
('NOTA2', '2025-10-09', 'G03', 'G02', '', '2025-10-21 02:08:43');

-- --------------------------------------------------------

--
-- Table structure for table `zmutasim`
--

CREATE TABLE `zmutasim` (
  `nonota` char(30) DEFAULT NULL,
  `kodebrg` char(20) DEFAULT NULL,
  `kodegd1` char(10) NOT NULL,
  `kodegd2` char(10) NOT NULL,
  `jlh1` double DEFAULT 0,
  `jlh2` double DEFAULT 0,
  `jlh3` double DEFAULT 0,
  `operator` char(10) DEFAULT '',
  `logtime` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zmutasim`
--

INSERT INTO `zmutasim` (`nonota`, `kodebrg`, `kodegd1`, `kodegd2`, `jlh1`, `jlh2`, `jlh3`, `operator`, `logtime`) VALUES
('NOTA1', 'ALB0001', 'G01', 'G02', 120, 0, 0, 'User', '2025-10-09 08:07:07'),
('NOTA2', 'CAR0035', 'G03', 'G02', 0, 0, 0, 'User', '2025-10-21 02:08:43');

-- --------------------------------------------------------

--
-- Table structure for table `zpenyesuaian`
--

CREATE TABLE `zpenyesuaian` (
  `nonota` char(30) DEFAULT NULL,
  `tgl` date DEFAULT NULL,
  `kodegd` char(10) DEFAULT NULL,
  `operator` char(10) DEFAULT '',
  `logtime` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zpenyesuaian`
--

INSERT INTO `zpenyesuaian` (`nonota`, `tgl`, `kodegd`, `operator`, `logtime`) VALUES
('NOTA 2', '2025-10-13', 'G01', '', '2025-10-13 06:52:08'),
('NOTA 1', '2025-10-11', 'G02', '', '2025-10-13 06:56:18');

-- --------------------------------------------------------

--
-- Table structure for table `zpenyesuaianm`
--

CREATE TABLE `zpenyesuaianm` (
  `nonota` char(30) DEFAULT NULL,
  `kodebrg` char(20) DEFAULT NULL,
  `kodegd` char(10) NOT NULL,
  `jlh1` double DEFAULT 0,
  `jlh2` double DEFAULT 0,
  `jlh3` double DEFAULT 0,
  `qty` double DEFAULT 0,
  `harga` double DEFAULT 0,
  `operator` char(10) DEFAULT '',
  `logtime` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zpenyesuaianm`
--

INSERT INTO `zpenyesuaianm` (`nonota`, `kodebrg`, `kodegd`, `jlh1`, `jlh2`, `jlh3`, `qty`, `harga`, `operator`, `logtime`) VALUES
('NOTA 2', 'HDD0048', 'G01', 100, 0, 0, 1000, 1100000, 'User', '2025-10-13 06:52:08'),
('NOTA 1', 'CAR0035', 'G02', 20, 20, 30, 1000, 30000, 'User', '2025-10-13 06:56:18');

-- --------------------------------------------------------

--
-- Table structure for table `zrbeli`
--

CREATE TABLE `zrbeli` (
  `nonota` char(30) DEFAULT NULL,
  `nofaktur` char(30) DEFAULT NULL,
  `tgl` date DEFAULT NULL,
  `kodesup` char(10) DEFAULT NULL,
  `kodegd` char(10) NOT NULL,
  `nilai` double DEFAULT 0,
  `lunas` int(11) DEFAULT 0,
  `tgltempo` date DEFAULT NULL,
  `ppn` double(10,2) DEFAULT 0.00,
  `hppn` double(10,2) DEFAULT 0.00,
  `disc1` double(10,2) DEFAULT 0.00,
  `hdisc1` double(10,2) DEFAULT 0.00,
  `disc2` double(10,2) DEFAULT 0.00,
  `hdisc2` double(10,2) DEFAULT 0.00,
  `disc3` double(10,2) DEFAULT 0.00,
  `hdisc3` double(10,2) DEFAULT 0.00,
  `ket` varchar(1000) DEFAULT NULL,
  `operator` char(10) DEFAULT '',
  `logtime` timestamp NOT NULL DEFAULT current_timestamp(),
  `lainnya` double(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zrbeli`
--

INSERT INTO `zrbeli` (`nonota`, `nofaktur`, `tgl`, `kodesup`, `kodegd`, `nilai`, `lunas`, `tgltempo`, `ppn`, `hppn`, `disc1`, `hdisc1`, `disc2`, `hdisc2`, `disc3`, `hdisc3`, `ket`, `operator`, `logtime`, `lainnya`) VALUES
('NOTA3', 'FAKTUR 1', '2025-09-25', 'S002', '', 1678302, 0, '2025-09-25', 11.00, 163345.27, 10.00, 203697.80, 10.00, 183328.02, 10.00, 164995.22, 'TEST NOTA 3', '', '2025-10-15 03:14:46', 30000.00);

-- --------------------------------------------------------

--
-- Table structure for table `zrbelim`
--

CREATE TABLE `zrbelim` (
  `nonota` char(30) DEFAULT NULL,
  `nofaktur` char(30) DEFAULT NULL,
  `kodebrg` char(20) DEFAULT NULL,
  `kodegd` char(10) NOT NULL,
  `jlh1` double DEFAULT 0,
  `jlh2` double DEFAULT 0,
  `jlh3` double DEFAULT 0,
  `harga` double DEFAULT 9,
  `disca` double DEFAULT 0,
  `hdisca` double DEFAULT 0,
  `discb` double DEFAULT 0,
  `hdiscb` double DEFAULT 0,
  `discc` double DEFAULT 0,
  `hdiscc` double DEFAULT 0,
  `discrp` double DEFAULT 0,
  `jumlah` double DEFAULT 0,
  `logtime` timestamp NOT NULL DEFAULT current_timestamp(),
  `operator` char(10) DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zrbelim`
--

INSERT INTO `zrbelim` (`nonota`, `nofaktur`, `kodebrg`, `kodegd`, `jlh1`, `jlh2`, `jlh3`, `harga`, `disca`, `hdisca`, `discb`, `hdiscb`, `discc`, `hdiscc`, `discrp`, `jumlah`, `logtime`, `operator`) VALUES
('NOTA3', 'FAKTUR 1', 'ALB0001', 'G02', 1, 0, 0, 3000000, 10, 0, 10, 0, 10, 0, 1000000, 1187000, '2025-10-15 03:14:46', 'User'),
('NOTA3', 'FAKTUR 1', 'BAT0002', 'G02', 1, 3, 0, 225000, 20, 0, 0, 0, 0, 0, 0, 234000, '2025-10-15 03:14:46', 'User'),
('NOTA3', 'FAKTUR 1', 'CAR0035', 'G03', 1, 2, 3, 30000, 10, 0, 10, 0, 0, 0, 0, 35478, '2025-10-15 03:14:46', 'User'),
('NOTA3', 'FAKTUR 1', 'KEY0004', 'G01', 1, 2, 3, 300000, 10, 0, 0, 0, 0, 0, 0, 580500, '2025-10-15 03:14:46', 'User');

-- --------------------------------------------------------

--
-- Table structure for table `zrjual`
--

CREATE TABLE `zrjual` (
  `nonota` char(30) DEFAULT NULL,
  `nofaktur` char(30) DEFAULT NULL,
  `tgl` date DEFAULT NULL,
  `kodekust` char(10) DEFAULT NULL,
  `kodesls` char(10) DEFAULT NULL,
  `kodegd` char(10) DEFAULT NULL,
  `nilai` double DEFAULT 0,
  `lunas` int(11) DEFAULT 0,
  `tgltempo` date DEFAULT NULL,
  `ppn` double(10,2) DEFAULT 0.00,
  `hppn` double(10,2) DEFAULT 0.00,
  `disc1` double(10,2) DEFAULT 0.00,
  `hdisc1` double(10,2) DEFAULT 0.00,
  `disc2` double(10,2) DEFAULT 0.00,
  `hdisc2` double(10,2) DEFAULT 0.00,
  `disc3` double(10,2) DEFAULT 0.00,
  `hdisc3` double(10,2) DEFAULT 0.00,
  `ket` varchar(1000) DEFAULT NULL,
  `operator` char(10) DEFAULT '',
  `logtime` timestamp NOT NULL DEFAULT current_timestamp(),
  `lainnya` double(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zrjual`
--

INSERT INTO `zrjual` (`nonota`, `nofaktur`, `tgl`, `kodekust`, `kodesls`, `kodegd`, `nilai`, `lunas`, `tgltempo`, `ppn`, `hppn`, `disc1`, `hdisc1`, `disc2`, `hdisc2`, `disc3`, `hdisc3`, `ket`, `operator`, `logtime`, `lainnya`) VALUES
('NOTARETUR1', 'NOTA1', '2025-10-18', 'C01', 'S01', NULL, 19973706, 0, '2025-10-18', 11.00, 1969466.40, 10.00, 2456000.00, 10.00, 2210400.00, 10.00, 1989360.00, 'TEST', '', '2025-10-18 03:19:19', 100000.00);

-- --------------------------------------------------------

--
-- Table structure for table `zrjualm`
--

CREATE TABLE `zrjualm` (
  `nonota` char(30) DEFAULT NULL,
  `nofaktur` char(30) DEFAULT NULL,
  `kodebrg` char(20) DEFAULT NULL,
  `kodegd` char(10) DEFAULT NULL,
  `jlh1` double DEFAULT 0,
  `jlh2` double DEFAULT 0,
  `jlh3` double DEFAULT 0,
  `harga` double DEFAULT 9,
  `disca` double DEFAULT 0,
  `hdisca` double DEFAULT 0,
  `discb` double DEFAULT 0,
  `hdiscb` double DEFAULT 0,
  `discc` double DEFAULT 0,
  `hdiscc` double DEFAULT 0,
  `discrp` double DEFAULT 0,
  `jumlah` double DEFAULT 0,
  `operator` char(10) DEFAULT '',
  `logtime` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zrjualm`
--

INSERT INTO `zrjualm` (`nonota`, `nofaktur`, `kodebrg`, `kodegd`, `jlh1`, `jlh2`, `jlh3`, `harga`, `disca`, `hdisca`, `discb`, `hdiscb`, `discc`, `hdiscc`, `discrp`, `jumlah`, `operator`, `logtime`) VALUES
('NOTARETUR1', 'NOTA1', 'ALB0001', 'G01', 10, 0, 0, 3000000, 10, 0, 10, 0, 10, 0, 10000, 21860000, 'User', '2025-10-18 03:19:19'),
('NOTARETUR1', 'NOTA1', 'BAT0002', 'G01', 12, 0, 0, 225000, 0, 0, 0, 0, 0, 0, 0, 2700000, 'User', '2025-10-18 03:19:19'),
('NOTARETUR1', 'NOTA1', 'ALB0001', 'G01', 10, 0, 0, 3000000, 10, 0, 10, 0, 10, 0, 10000, 21860000, 'User', '2025-10-17 20:19:19'),
('NOTARETUR1', 'NOTA1', 'ALB0001', 'G02', 1, 0, 0, 3000000, 10, 0, 10, 0, 10, 0, 1000000, 1187000, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'BAT0002', 'G02', 1, 3, 0, 225000, 20, 0, 0, 0, 0, 0, 0, 234000, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'CAR0079', 'G02', 1, 2, 3, 50000, 0, 0, 0, 0, 0, 0, 0, 60750, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'CAR0035', 'G03', 1, 2, 3, 30000, 10, 0, 10, 0, 0, 0, 0, 35478, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'CAR0043', 'G03', 1, 0, 0, 250000, 0, 0, 0, 0, 0, 0, 0, 250000, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'KAB0064', 'G02', 1, 2, 0, 20000, 10, 0, 10, 0, 0, 0, 0, 16524, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'KAB0064', 'G02', 1, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 20000, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'KEY0004', 'G01', 1, 2, 3, 300000, 10, 0, 0, 0, 0, 0, 0, 580500, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'PAP0008', 'G01', 2, 0, 0, 110000, 0, 0, 0, 0, 0, 0, 0, 220000, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'PAP0010', 'G03', 10, 0, 0, 3400, 0, 0, 0, 0, 0, 0, 0, 34000, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'HDD0048', 'G01', 1, 0, 0, 1100000, 0, 0, 0, 0, 0, 0, 0, 1100000, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'TIN0081', 'G03', 1, 1, 0, 60000, 0, 0, 0, 0, 0, 0, 0, 72000, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'CAR0079', 'G02', 1, 2, 3, 50000, 0, 0, 0, 0, 0, 0, 0, 60750, 'User', '2025-10-03 18:46:21'),
('NOTARETUR1', 'NOTA1', 'BAT0002', 'G01', 12, 0, 0, 225000, 0, 0, 0, 0, 0, 0, 0, 2700000, 'User', '2025-10-17 20:19:19');

-- --------------------------------------------------------

--
-- Table structure for table `zsaldo`
--

CREATE TABLE `zsaldo` (
  `kodebrg` char(18) DEFAULT NULL,
  `kodegd` char(10) DEFAULT NULL,
  `awal1` decimal(10,0) DEFAULT 0,
  `awal2` decimal(10,0) DEFAULT 0,
  `masuk1` decimal(10,0) DEFAULT 0,
  `masuk2` decimal(10,0) DEFAULT 0,
  `keluar1` decimal(10,0) DEFAULT 0,
  `keluar2` decimal(10,0) DEFAULT 0,
  `sisa1` decimal(10,0) DEFAULT 0,
  `sisa2` decimal(10,0) DEFAULT 0,
  `sisa3` decimal(10,0) DEFAULT 0,
  `sisa` decimal(10,0) DEFAULT 0,
  `qty` decimal(10,0) DEFAULT 0,
  `qty1` decimal(10,0) DEFAULT 0,
  `autoid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `zsales`
--

CREATE TABLE `zsales` (
  `kodesls` char(20) NOT NULL,
  `namasls` char(50) NOT NULL,
  `alamat` char(255) NOT NULL,
  `kota` char(50) NOT NULL,
  `ktp` varchar(20) NOT NULL,
  `npwp` varchar(30) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `zsales`
--

INSERT INTO `zsales` (`kodesls`, `namasls`, `alamat`, `kota`, `ktp`, `npwp`) VALUES
('S01', 'SALES1', 'INDAH', 'PONTIANAK', 'TEST', 'TEST'),
('S02', 'SALES2', 'MENTARI', 'PONTIANAK', 'TEST', 'TEST');

-- --------------------------------------------------------

--
-- Table structure for table `zstok`
--

CREATE TABLE `zstok` (
  `kodebrg` char(20) NOT NULL,
  `kodemerk` char(20) DEFAULT NULL,
  `kodegol` char(20) DEFAULT NULL,
  `kodegrup` char(10) DEFAULT NULL,
  `namabrg` varchar(50) NOT NULL,
  `satuan1` char(10) NOT NULL,
  `satuan2` char(10) NOT NULL,
  `satuan3` char(10) NOT NULL,
  `isi1` double NOT NULL,
  `isi2` double NOT NULL,
  `hrgbeli` double DEFAULT 0,
  `harga1` double DEFAULT NULL,
  `harga11` double DEFAULT NULL,
  `harga111` double DEFAULT NULL,
  `harga2` double DEFAULT NULL,
  `harga22` double DEFAULT NULL,
  `harga222` double DEFAULT NULL,
  `harga3` double DEFAULT NULL,
  `harga33` double DEFAULT NULL,
  `harga333` double DEFAULT NULL,
  `harga4` double DEFAULT NULL,
  `harga44` double DEFAULT NULL,
  `harga444` double DEFAULT NULL,
  `harga5` double DEFAULT NULL,
  `harga55` double DEFAULT NULL,
  `harga555` double DEFAULT NULL,
  `harga6` double DEFAULT NULL,
  `harga66` double DEFAULT NULL,
  `harga666` double DEFAULT NULL,
  `autoid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `zstok`
--

INSERT INTO `zstok` (`kodebrg`, `kodemerk`, `kodegol`, `kodegrup`, `namabrg`, `satuan1`, `satuan2`, `satuan3`, `isi1`, `isi2`, `hrgbeli`, `harga1`, `harga11`, `harga111`, `harga2`, `harga22`, `harga222`, `harga3`, `harga33`, `harga333`, `harga4`, `harga44`, `harga444`, `harga5`, `harga55`, `harga555`, `harga6`, `harga66`, `harga666`, `autoid`) VALUES
('KER0001', 'M01', 'GOL01', 'A01', 'A4 70GSM', '-', '-', '-', 0, 0, 0, 35000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1),
('TEN0002', 'M01', 'GOL01', 'A01', 'ACC POINT TENDA WH302A', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2),
('ACC0004', 'M01', 'GOL01', 'A01', 'ACCES POINT WIRELESS TP-LINK', '-', '-', '-', 0, 0, 0, 485000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3),
('ACC0005', 'M01', 'GOL01', 'A01', 'ACCESS POINT DLINK DAP-1360', '-', '-', '-', 0, 0, 0, 505000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4),
('ADA0007', 'M01', 'GOL01', 'A01', 'ADAPTOR APPLE', '-', '-', '-', 0, 0, 0, 680000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5),
('ADA0003', 'M01', 'GOL01', 'A01', 'ADAPTOR CAMERA', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6),
('ADA0004', 'M01', 'GOL01', 'A01', 'ADAPTOR NANO STATION', '-', '-', '-', 0, 0, 0, 350000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7),
('ADA0002', 'M01', 'GOL01', 'A01', 'ADAPTOR NOTE BOOK/ UNIVERSAL', '-', '-', '-', 0, 0, 0, 95000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8),
('ADA00005', 'M01', 'GOL01', 'A01', 'ADAPTOR POS PRINTER', '-', '-', '-', 0, 0, 0, 650000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9),
('ADA0008', 'M01', 'GOL01', 'A01', 'ADAPTOR PRINTER HP', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10),
('DLI0002', 'M01', 'GOL01', 'A01', 'ADSL WIRELESS DSL2750E 2ANTENA', '-', '-', '-', 0, 0, 0, 450000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11),
('ZALA0003', 'M01', 'GOL01', 'A01', 'ALAT-ALAT LISTRIK', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12),
('ALB0002', 'M01', 'GOL01', 'A01', 'ALBOX  PD', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 13),
('ALB0001', 'M01', 'GOL01', 'A01', 'ALBOX ACP', 'DUS', '-', '-', 1, 0, 3000000, 3150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14),
('ALB0003', 'M01', 'GOL01', 'A01', 'ALBOX BS', '-', '-', '-', 0, 0, 0, 1500000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15),
('ALB0007', 'M01', 'GOL01', 'A01', 'ALBOX KEYPAD', '-', '-', '-', 0, 0, 0, 750000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16),
('ALB0004', 'M01', 'GOL01', 'A01', 'ALBOX PB', '-', '-', '-', 0, 0, 0, 150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 17),
('ALL0001', 'M01', 'GOL01', 'A01', 'ALL IN ONE PC HP', '-', '-', '-', 0, 0, 0, 7899000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 18),
('ANT0006', 'M01', 'GOL01', 'A01', 'ANTI VIRUS KASPERSKY', '-', '-', '-', 0, 0, 0, 375000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 19),
('ANT0007', 'M01', 'GOL01', 'A01', 'ANTI VIRUS KASPERSKY SMALL OFFICE', '-', '-', '-', 0, 0, 0, 2850000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20),
('AUD0001', 'M01', 'GOL01', 'A01', 'AUDIO MICROPON', '-', '-', '-', 0, 0, 0, 125000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 21),
('AUD0002', 'M01', 'GOL01', 'A01', 'AUDIO MICROPON (BULAT)', '-', '-', '-', 0, 0, 0, 150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 22),
('SPE0031', 'M01', 'GOL01', 'A01', 'AUDIOPAD DAG 8', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 23),
('ZBIA0002', 'M01', 'GOL01', 'A01', 'BAHAN-BAHAN', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 24),
('BARANG BARU', 'M01', 'GOL01', 'A01', 'BARANG BARU', '-', '-', '-', 0, 0, 0, 535000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25),
('ZZZZZ', 'M01', 'GOL01', 'A01', 'BARANG DISCOUNT', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 26),
('BAT0001', 'M01', 'GOL01', 'A01', 'BATERAI MAXELL/CMOS.', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 27),
('BAT0004', 'M01', 'GOL01', 'A01', 'BATERAI NOTEBOOK', '-', '-', '-', 0, 0, 0, 480000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 28),
('BAT0003', 'M01', 'GOL01', 'A01', 'BATERAI UPS 4,5AH', '-', '-', '-', 0, 0, 0, 185000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 29),
('BAT0002', 'M01', 'GOL01', 'A01', 'BATERAI UPS 7AH-12V', 'SET', '', '-', 10, 0, 225000, 185000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30),
('ZBIA0001', 'M01', 'GOL01', 'A01', 'BIAYA PEMASANGAN', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 31),
('BLU0001', 'M01', 'GOL01', 'A01', 'BLUETOOTH USB USB MINI', '-', '-', '-', 0, 0, 0, 30000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32),
('POW0025', 'M01', 'GOL01', 'A01', 'BPS ARNEY  FOR DVR 4CH', '-', '-', '-', 0, 0, 0, 446000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 33),
('POW0027', 'M01', 'GOL01', 'A01', 'BPS ARNEY FOR DVR 16CH', '-', '-', '-', 0, 0, 0, 850000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 34),
('POW0026', 'M01', 'GOL01', 'A01', 'BPS ARNEY FOR DVR 8 CH', '-', '-', '-', 0, 0, 0, 600000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 35),
('BRA0001', 'M01', 'GOL01', 'A01', 'BRACKET FOR LOCKS ST600', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 36),
('BRA0002', 'M01', 'GOL01', 'A01', 'BRACKET FOR LOCKS ST700', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 37),
('CAM0046', 'M01', 'GOL01', 'A01', 'CAM BULLET TRI VB 102 OUT DOOR', '-', '-', '-', 0, 0, 0, 325000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 38),
('CAM0047', 'M01', 'GOL01', 'A01', 'CAM DOME TRI VD 101 INDOOR', '-', '-', '-', 0, 0, 0, 400000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 39),
('CAM0048', 'M01', 'GOL01', 'A01', 'CAM ID KEEPER KN 130', '-', '-', '-', 0, 0, 0, 625000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40),
('CAM0049', 'M01', 'GOL01', 'A01', 'CAM ID KEEPER KN 200', '-', '-', '-', 0, 0, 0, 525000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 41),
('CAM0052', 'M01', 'GOL01', 'A01', 'CAM INDOOR STEALTH/HELIOS', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 42),
('CAM0051', 'M01', 'GOL01', 'A01', 'CAM KEEPER KF 200AHD INDOOR', '-', '-', '-', 0, 0, 0, 935000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 43),
('CAM0038', 'M01', 'GOL01', 'A01', 'CAM KEEPER OQ 130', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 44),
('CAM0036', 'M01', 'GOL01', 'A01', 'CAM KEEPER OT 100', '-', '-', '-', 0, 0, 0, 425000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 45),
('CAM0042', 'M01', 'GOL01', 'A01', 'CAM TRI VD 308', '-', '-', '-', 0, 0, 0, 550000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 46),
('CAM0018', 'M01', 'GOL01', 'A01', 'CAMERA  IR TELVIEW 420TVL', '-', '-', '-', 0, 0, 0, 775000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 47),
('CAM0050', 'M01', 'GOL01', 'A01', 'CAMERA  OUT DOOR KEEPER KE 200 AHD', '-', '-', '-', 0, 0, 0, 625000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 48),
('CAM0034', 'M01', 'GOL01', 'A01', 'CAMERA BULLET', '-', '-', '-', 0, 0, 0, 250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 49),
('CAM0053', 'M01', 'GOL01', 'A01', 'CAMERA DAHUA OUTDOOR 1MP RP-53', '-', '-', '-', 0, 0, 0, 250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50),
('CAM0040', 'M01', 'GOL01', 'A01', 'CAMERA DOME 3 ARAY', '-', '-', '-', 0, 0, 0, 250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 51),
('CAM0054', 'M01', 'GOL01', 'A01', 'CAMERA IN DOOR  DOME TRI PD 10', '-', '-', '-', 0, 0, 0, 425000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 52),
('CAM0012', 'M01', 'GOL01', 'A01', 'CAMERA INFRARED F-36', '-', '-', '-', 0, 0, 0, 450000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 53),
('CAM0044', 'M01', 'GOL01', 'A01', 'CAMERA IP TRIVISION OUTDOOR', '-', '-', '-', 0, 0, 0, 1150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 54),
('CAM0030', 'M01', 'GOL01', 'A01', 'CAMERA KC-OS3180', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 55),
('CAM0035', 'M01', 'GOL01', 'A01', 'CAMERA KEEPER ID 100', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 56),
('CAM0045', 'M01', 'GOL01', 'A01', 'CAMERA KEEPER OD 130', '-', '-', '-', 0, 0, 0, 620000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 57),
('CAM0037', 'M01', 'GOL01', 'A01', 'CAMERA KEEPER OD 3190/ OD100', '-', '-', '-', 0, 0, 0, 425000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 58),
('CAM0039', 'M01', 'GOL01', 'A01', 'CAMERA KEEPER UD 100', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 59),
('CAM0015', 'M01', 'GOL01', 'A01', 'CAMERA MINI (PENGINTAI)', '-', '-', '-', 0, 0, 0, 475000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 60),
('CAM0032', 'M01', 'GOL01', 'A01', 'CAMERA OH 3110 (2 INFRA RED)', '-', '-', '-', 0, 0, 0, 850000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 61),
('CAM0041', 'M01', 'GOL01', 'A01', 'CAMERA TRI VB 309', '-', '-', '-', 0, 0, 0, 550000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 62),
('CAM0013', 'M01', 'GOL01', 'A01', 'CAMERA WIRELESS/IP CAM 2CU/1MP', '-', '-', '-', 0, 0, 0, 850000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 63),
('CAR0067', 'M01', 'GOL01', 'A01', 'CARD HIGH CO', '-', '-', '-', 0, 0, 0, 2250, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64),
('CAR0001', 'M01', 'GOL01', 'A01', 'CARD READER', '-', '-', '-', 0, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 65),
('CAR0066', 'M01', 'GOL01', 'A01', 'CARD READER (PEMBACA ID CARD)', '-', '-', '-', 0, 0, 0, 1000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 66),
('CAR0011', 'M01', 'GOL01', 'A01', 'CARTIDGE INK HP C87 28A / HP28 COLOR', '-', '-', '-', 0, 0, 0, 245000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 67),
('CAR0079', 'M01', 'GOL01', 'A01', 'CARTRID EPSON LX310/LQ800', 'DUS', 'PACK', 'PCS', 10, 20, 50000, 45000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 68),
('CAR0035', 'M01', 'GOL01', 'A01', 'CARTRIDGE  EPSON 8750', 'BOX', 'PACK', 'PCS', 5, 10, 30000, 35000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 69),
('CAR0097', 'M01', 'GOL01', 'A01', 'CARTRIDGE  HP 97 COLOR', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 70),
('CAR0085', 'M01', 'GOL01', 'A01', 'CARTRIDGE 73N B', '-', '-', '-', 0, 0, 0, 130000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 71),
('CAR0086', 'M01', 'GOL01', 'A01', 'CARTRIDGE 73N C', '-', '-', '-', 0, 0, 0, 130000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 72),
('CAR0087', 'M01', 'GOL01', 'A01', 'CARTRIDGE 73N M', '-', '-', '-', 0, 0, 0, 130000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 73),
('CAR0088', 'M01', 'GOL01', 'A01', 'CARTRIDGE 73N Y', '-', '-', '-', 0, 0, 0, 130000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 74),
('CAR0089', 'M01', 'GOL01', 'A01', 'CARTRIDGE 8750 PRINTECH', '-', '-', '-', 0, 0, 0, 45000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 75),
('CAR0070', 'M01', 'GOL01', 'A01', 'CARTRIDGE CANON 725BK', '-', '-', '-', 0, 0, 0, 105000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 76),
('CAR0077', 'M01', 'GOL01', 'A01', 'CARTRIDGE CANON 88 BLACK', '-', '-', '-', 0, 0, 0, 190000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 77),
('CAR0078', 'M01', 'GOL01', 'A01', 'CARTRIDGE CANON 98 BLACK', '-', '-', '-', 0, 0, 0, 220000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 78),
('CAR0074', 'M01', 'GOL01', 'A01', 'CARTRIDGE CANON CL-741 COLOR', '-', '-', '-', 0, 0, 0, 285000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 79),
('CAR0050', 'M01', 'GOL01', 'A01', 'CARTRIDGE CANON CL-811', '-', '-', '-', 0, 0, 0, 250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 80),
('CAR0005', 'M01', 'GOL01', 'A01', 'CARTRIDGE CANON CL-831', '-', '-', '-', 0, 0, 0, 240000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 81),
('CAR0075', 'M01', 'GOL01', 'A01', 'CARTRIDGE CANON PG-740 BLACK', '-', '-', '-', 0, 0, 0, 195000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 82),
('CAR0051', 'M01', 'GOL01', 'A01', 'CARTRIDGE CANON PG-810', '-', '-', '-', 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 83),
('CAR0006', 'M01', 'GOL01', 'A01', 'CARTRIDGE CANON PG-830', '-', '-', '-', 0, 0, 0, 190000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 84),
('CAR0084', 'M01', 'GOL01', 'A01', 'CARTRIDGE CANON PG 745 BLACK', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 85),
('CAR0101', 'M01', 'GOL01', 'A01', 'CARTRIDGE CANON, CL 726', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 86),
('CAR0041', 'M01', 'GOL01', 'A01', 'CARTRIDGE EPSON LQ-2180 (ASLI)', '-', '-', '-', 0, 0, 0, 135000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 87),
('CAR0004', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP-22 COLOR', '-', '-', '-', 0, 0, 0, 165000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 88),
('CAR0043', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP-60 BLACK', 'BOX', '', '-', 12, 0, 250000, 205000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 89),
('CAR0047', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP-60 COLOR', '-', '-', '-', 0, 0, 0, 245000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 90),
('CAR0025', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP 27B', '-', '-', '-', 0, 0, 0, 250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 91),
('CAR0076', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP 564 BLACK', '-', '-', '-', 0, 0, 0, 135000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 92),
('CAR0081', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP 564 CYAN', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 93),
('CAR0083', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP 564 MAGENTA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 94),
('CAR0099', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP 704 COLOR/BLACK', '-', '-', '-', 0, 0, 0, 120000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 95),
('CAR0057', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP 802BK', '-', '-', '-', 0, 0, 0, 95000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 96),
('CAR0058', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP 802CL', '-', '-', '-', 0, 0, 0, 145000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 97),
('CAR0098', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP 901', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 98),
('CAR0100', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP678 BLACK/COLOR', '-', '-', '-', 0, 0, 0, 115000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 99),
('CAR0064', 'M01', 'GOL01', 'A01', 'CARTRIDGE HP901 BLACK', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100),
('CAR0029', 'M01', 'GOL01', 'A01', 'CARTRIDGE UTK PLQ 20', '-', '-', '-', 0, 0, 0, 120000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 101),
('CAS0019', 'M01', 'GOL01', 'A01', 'CASE DAZUMBA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 102),
('CAS0024', 'M01', 'GOL01', 'A01', 'CASE DAZUMBA D VITO 696', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 103),
('CAS0010', 'M01', 'GOL01', 'A01', 'CASE EXTERNAL 2.5\" (S-ATA )', '-', '-', '-', 0, 0, 0, 135000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 104),
('CAS0011', 'M01', 'GOL01', 'A01', 'CASE SIMBADDA SIMCOOL', '-', '-', '-', 0, 0, 0, 595000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 105),
('CAS0009', 'M01', 'GOL01', 'A01', 'CASH DRAWER', '-', '-', '-', 0, 0, 0, 1100000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 106),
('CAS0018', 'M01', 'GOL01', 'A01', 'CASING POWER LOGIC', '-', '-', '-', 0, 0, 0, 310000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 107),
('CAS0017', 'M01', 'GOL01', 'A01', 'CASING POWER LOGIC AZZURA (MINI)', '-', '-', '-', 0, 0, 0, 250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 108),
('CCT0002', 'M01', 'GOL01', 'A01', 'CCTV BOX POWER', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 109),
('CCT0003', 'M01', 'GOL01', 'A01', 'CCTV HOUSING & BRAKET', '-', '-', '-', 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 110),
('CCT0004', 'M01', 'GOL01', 'A01', 'CCTV MIC', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 111),
('CCT0001', 'M01', 'GOL01', 'A01', 'CCTV SPEAKER', '-', '-', '-', 0, 0, 0, 335000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 112),
('CDR0007', 'M01', 'GOL01', 'A01', 'CD-R BLANK PRINTECH', '-', '-', '-', 0, 0, 0, 3000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 113),
('CDB0001', 'M01', 'GOL01', 'A01', 'CD BAG', '-', '-', '-', 0, 0, 0, 12000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 114),
('CDR0008', 'M01', 'GOL01', 'A01', 'CD R VERBATIM', '-', '-', '-', 0, 0, 0, 3500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 115),
('CON0022', 'M01', 'GOL01', 'A01', 'CF  NUSA 2PLY PRS', '-', '-', '-', 0, 0, 0, 210000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 116),
('CON0031', 'M01', 'GOL01', 'A01', 'CF 9 1/2 X 11 1/2 PER2 2PLY (H/2 & V/2)', '-', '-', '-', 0, 0, 0, 350000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 117),
('CON0032', 'M01', 'GOL01', 'A01', 'CF NUSA 1PLY 9 1/2 X11', '-', '-', '-', 0, 0, 0, 140000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 118),
('CON0034', 'M01', 'GOL01', 'A01', 'CF NUSA 2PLY (K2P)', '-', '-', '-', 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 119),
('CON0026', 'M01', 'GOL01', 'A01', 'CF NUSA 3 PLY PRS W', '-', '-', '-', 0, 0, 0, 330000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 120),
('CON0033', 'M01', 'GOL01', 'A01', 'CF NUSA 4PLY PRS NCR WARNA UK 9 1/2X11', '-', '-', '-', 0, 0, 0, 250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 121),
('CON0016', 'M01', 'GOL01', 'A01', 'CF OFFICE PRINT 3PLY PRS WARNA', '-', '-', '-', 0, 0, 0, 295000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 122),
('CON0028', 'M01', 'GOL01', 'A01', 'CONECTOR  I /SAMBUNGAN I', '-', '-', '-', 0, 0, 0, 7000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 123),
('CON1001', 'M01', 'GOL01', 'A01', 'CONECTOR BNC/DC', '-', '-', '-', 0, 0, 0, 6000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 124),
('CON0029', 'M01', 'GOL01', 'A01', 'CONECTOR RJ11', '-', '-', '-', 0, 0, 0, 850, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 125),
('CON0011', 'M01', 'GOL01', 'A01', 'CONNECTOR BNC 2RCA', '-', '-', '-', 0, 0, 0, 6000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 126),
('CON0008', 'M01', 'GOL01', 'A01', 'CONNECTOR BNC.', '-', '-', '-', 0, 0, 0, 6000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 127),
('CON0001', 'M01', 'GOL01', 'A01', 'CONNECTOR RJ 45 ORIGINAL.', '-', '-', '-', 0, 0, 0, 5000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128),
('CON0017', 'M01', 'GOL01', 'A01', 'CONNECTOR RJ45 STANDARD', '-', '-', '-', 0, 0, 0, 5000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 129),
('CON0024', 'M01', 'GOL01', 'A01', 'CONT FORM  SIDU 5 PLY NCR WARNA', '-', '-', '-', 0, 0, 0, 330000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 130),
('CON0003', 'M01', 'GOL01', 'A01', 'CONT FORM SIDU 1 PLY', '-', '-', '-', 0, 0, 0, 150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 131),
('CON0023', 'M01', 'GOL01', 'A01', 'CONT FORM SIDU 1 PLY PRS', '-', '-', '-', 0, 0, 0, 134000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 132),
('CON0020', 'M01', 'GOL01', 'A01', 'CONT FORM SIDU 2 PLY PRS W (K2W PRS)', '-', '-', '-', 0, 0, 0, 235000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 133),
('CON0025', 'M01', 'GOL01', 'A01', 'CONT FORM SIDU 2 PLY WR (K2W)', '-', '-', '-', 0, 0, 0, 230000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 134),
('CON0005', 'M01', 'GOL01', 'A01', 'CONT FORM SIDU 3 PLY POLOS(K3P)', '-', '-', '-', 0, 0, 0, 325000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 135),
('CON0002', 'M01', 'GOL01', 'A01', 'CONT FORM SIDU 3 PLY PRS WARNA (K3WPRS)', '-', '-', '-', 0, 0, 0, 355000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 136),
('CON0009', 'M01', 'GOL01', 'A01', 'CONT FORM SIDU 4PLY   WARNA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 137),
('CON0021', 'M01', 'GOL01', 'A01', 'CONT FORM SIDU 4PLY NCR PRS (4WPRS)', '-', '-', '-', 0, 0, 0, 260000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 138),
('CON0027', 'M01', 'GOL01', 'A01', 'CONT FORM SIDU K 3 W', '-', '-', '-', 0, 0, 0, 340000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 139),
('CON0030', 'M01', 'GOL01', 'A01', 'CONVERTER IDE TO SATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 140),
('CRI0001', 'M01', 'GOL01', 'A01', 'CRIMPING TOOL', '-', '-', '-', 0, 0, 0, 65000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 141),
('DAT0001', 'M01', 'GOL01', 'A01', 'DATA SWITCH 1-2 PORT', '-', '-', '-', 0, 0, 0, 110000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 142),
('RAM0052', 'M01', 'GOL01', 'A01', 'DDR2 2GB VENOM', '-', '-', '-', 0, 0, 0, 255000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 143),
('RAM0050', 'M01', 'GOL01', 'A01', 'DDR3 2GB VENOM', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 144),
('RAM0049', 'M01', 'GOL01', 'A01', 'DDR3 4GB PC 10600 VENOM', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 145),
('RAM0053', 'M01', 'GOL01', 'A01', 'DDR4 8GB KINGSTON', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 146),
('RAM0051', 'M01', 'GOL01', 'A01', 'DDR4 8GB PC17000 HYNIX', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 147),
('DLI0001', 'M01', 'GOL01', 'A01', 'DLINK ROUTER WIRELESS DIR612', '-', '-', '-', 0, 0, 0, 350000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 148),
('DVD0012', 'M01', 'GOL01', 'A01', 'DVD-R VERBATIM', '-', '-', '-', 0, 0, 0, 4000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 149),
('DVD0017', 'M01', 'GOL01', 'A01', 'DVD PORTABLE HIRICE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 150),
('DVD0007', 'M01', 'GOL01', 'A01', 'DVD RW LG', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 151),
('DVD0016', 'M01', 'GOL01', 'A01', 'DVD RW LITE-ON TRAY', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 152),
('DVD0014', 'M01', 'GOL01', 'A01', 'DVDRW EXTERNAL BUFFALO', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 153),
('DVD0015', 'M01', 'GOL01', 'A01', 'DVDRW EXTERNAL SAMSUNG', '-', '-', '-', 0, 0, 0, 330000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 154),
('DVD0011', 'M01', 'GOL01', 'A01', 'DVDRW LG EXTERNAL', '-', '-', '-', 0, 0, 0, 330000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 155),
('DVR0006', 'M01', 'GOL01', 'A01', 'DVR  HS 6996 (16 CHANNEL)', '-', '-', '-', 0, 0, 0, 2870000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 156),
('DVR0020', 'M01', 'GOL01', 'A01', 'DVR  NEUTRAL 8CHL', '-', '-', '-', 0, 0, 0, 1525000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 157),
('DVR0015', 'M01', 'GOL01', 'A01', 'DVR 16 CH I-CATCH', '-', '-', '-', 0, 0, 0, 3425000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 158),
('DVR0011', 'M01', 'GOL01', 'A01', 'DVR 16 CH KEEPER AHD 3216', '-', '-', '-', 0, 0, 0, 2700000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 159),
('DVR0012', 'M01', 'GOL01', 'A01', 'DVR 16 CH TRIVISION', '-', '-', '-', 0, 0, 0, 3000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 160),
('DVR0018', 'M01', 'GOL01', 'A01', 'DVR 4 CH I-CATCH', '-', '-', '-', 0, 0, 0, 2550000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 161),
('DVR0019', 'M01', 'GOL01', 'A01', 'DVR 4 CH NEUTRAL', '-', '-', '-', 0, 0, 0, 1266000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 162),
('DVR0001', 'M01', 'GOL01', 'A01', 'DVR 4CHANNEL', '-', '-', '-', 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 163),
('DVR0014', 'M01', 'GOL01', 'A01', 'DVR 8 CH I-CATCH', '-', '-', '-', 0, 0, 0, 1950000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 164),
('DVR0002', 'M01', 'GOL01', 'A01', 'DVR 8CHANNEL', '-', '-', '-', 0, 0, 0, 1150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 165),
('DVR0008', 'M01', 'GOL01', 'A01', 'DVR HS 4996', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 166),
('DVR0007', 'M01', 'GOL01', 'A01', 'DVR HS 8996', '-', '-', '-', 0, 0, 0, 2300000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 167),
('DVR0010', 'M01', 'GOL01', 'A01', 'DVR KEEPER 3004/3104', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 168),
('DVR0009', 'M01', 'GOL01', 'A01', 'DVR KEEPER 3008/3108', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 169),
('DVR0017', 'M01', 'GOL01', 'A01', 'DVR TRI 4 CH', '-', '-', '-', 0, 0, 0, 1400000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 170),
('DVR0013', 'M01', 'GOL01', 'A01', 'DVR TRI 8 CH', '-', '-', '-', 0, 0, 0, 2000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 171),
('ECR 0001', 'M01', 'GOL01', 'A01', 'ECR SHARP', '-', '-', '-', 0, 0, 0, 4350000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 172),
('EDI0001', 'M01', 'GOL01', 'A01', 'EDIMAX WIRELESS REMOTE CONTROL', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 173),
('ENT0001', 'M01', 'GOL01', 'A01', 'ENTERPHONE', '-', '-', '-', 0, 0, 0, 700000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 174),
('FLA0047', 'M01', 'GOL01', 'A01', 'F-DISK AUV 130 ADATA 32GB', '-', '-', '-', 0, 0, 0, 175000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 175),
('FLA0047K', 'M01', 'GOL01', 'A01', 'F-DISK AUV130-325-RGD 32GB ADATA', '-', '-', '-', 0, 0, 0, 175000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 176),
('FLA0046K', 'M01', 'GOL01', 'A01', 'F-DISK AUV130/140 16GB ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 177),
('KER0002', 'M01', 'GOL01', 'A01', 'F4 70GSM SIDU/OP', '-', '-', '-', 0, 0, 0, 40000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 178),
('FLA0046', 'M01', 'GOL01', 'A01', 'FD AUV130/140 16 GB ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 179),
('FLA0034', 'M01', 'GOL01', 'A01', 'FD KINGSTON 8GB', '-', '-', '-', 0, 0, 0, 70000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 180),
('FLA0049', 'M01', 'GOL01', 'A01', 'FD SANDISK 64GB', '-', '-', '-', 0, 0, 0, 225000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 181),
('FLA0037', 'M01', 'GOL01', 'A01', 'FD SANDISK 8GB', '-', '-', '-', 0, 0, 0, 70000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 182),
('FLA0039K', 'M01', 'GOL01', 'A01', 'FD SANDISK CRUISER BLADE 16GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 183),
('FLA0038K', 'M01', 'GOL01', 'A01', 'FD SANDISK CRUIZER BLADE 4GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 184),
('FLA0037K', 'M01', 'GOL01', 'A01', 'FD SANDISK CRUIZER BLADE 8GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 185),
('FLA0040K', 'M01', 'GOL01', 'A01', 'FD SANDISK CRUIZER SWITCH  8GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 186),
('FLA0044K', 'M01', 'GOL01', 'A01', 'FD SANDISK CRUZER BLADE 32GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 187),
('FLA0041K', 'M01', 'GOL01', 'A01', 'FD SANDISK CRUZER SWITCH 16GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 188),
('FLA0042K', 'M01', 'GOL01', 'A01', 'FD SANDISK CRUZER SWITCH 32GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 189),
('FIN0001', 'M01', 'GOL01', 'A01', 'FINGERSPOT', '-', '-', '-', 0, 0, 0, 2750000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 190),
('FLA0035', 'M01', 'GOL01', 'A01', 'FLASDISK ADVAN16GB', '-', '-', '-', 0, 0, 0, 100000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 191),
('FLA0048', 'M01', 'GOL01', 'A01', 'FLASH DISK  VGEN 32GB', '-', '-', '-', 0, 0, 0, 120000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192),
('FLA0029', 'M01', 'GOL01', 'A01', 'FLASH DISK 16GB V-GEN', '-', '-', '-', 0, 0, 0, 90000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 193),
('FLA0010', 'M01', 'GOL01', 'A01', 'FLASH DISK 8GB V-GEN', '-', '-', '-', 0, 0, 0, 75000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 194),
('FLA0022', 'M01', 'GOL01', 'A01', 'FLASH DISK APACER 4GB AH128 & 129', '-', '-', '-', 0, 0, 0, 55000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 195),
('FLA0033', 'M01', 'GOL01', 'A01', 'FLASH DISK HP 16GB', '-', '-', '-', 0, 0, 0, 130000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 196),
('FLA0030', 'M01', 'GOL01', 'A01', 'FLASH DISK TOSHIBA 4GB', '-', '-', '-', 0, 0, 0, 45000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 197),
('FLA0031', 'M01', 'GOL01', 'A01', 'FLASH DISK TOSHIBA 8GB', '-', '-', '-', 0, 0, 0, 70000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 198),
('FLA0023', 'M01', 'GOL01', 'A01', 'FLASHDISK HP 8GB', '-', '-', '-', 0, 0, 0, 90000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 199),
('FLA0051', 'M01', 'GOL01', 'A01', 'FLASHDISK KINGSTON 32GB', '-', '-', '-', 0, 0, 0, 145000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 200),
('FLA0044', 'M01', 'GOL01', 'A01', 'FLASHDISK SANDISK CRUISER BLADE 32GB', '-', '-', '-', 0, 0, 0, 145000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 201),
('FLA0045', 'M01', 'GOL01', 'A01', 'FLASHDISK TOSHIBA 16GB', '-', '-', '-', 0, 0, 0, 90000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 202),
('FLA0050', 'M01', 'GOL01', 'A01', 'FLASHDISK TOSHIBA 32GB', '-', '-', '-', 0, 0, 0, 145000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 203),
('TIN0078', 'M01', 'GOL01', 'A01', 'FRESH INK CANON 250 ML BLACK', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 204),
('TIN0079', 'M01', 'GOL01', 'A01', 'FRESH INK CANON 250 ML CYAN', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 205),
('TIN0080', 'M01', 'GOL01', 'A01', 'FRESH INK CANON 250 ML MAGENTA', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 206),
('TIN0081', 'M01', 'GOL01', 'A01', 'FRESH INK CANON 250 ML YELLOW', 'DUS', 'PCS', '', 5, 10, 60000, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 207),
('GAM0002', 'M01', 'GOL01', 'A01', 'GAME PAD DOUBLE', '-', '-', '-', 0, 0, 0, 65000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 208),
('HDD0057', 'M01', 'GOL01', 'A01', 'HDD  NH13 1TB ADATA', '-', '-', '-', 0, 0, 0, 890000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 209),
('HDD0059', 'M01', 'GOL01', 'A01', 'HDD  SURVEILANCE 3TB WD', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 210),
('HDD0047', 'M01', 'GOL01', 'A01', 'HDD  TOSHIBA 500 GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 211),
('HDD0060', 'M01', 'GOL01', 'A01', 'HDD 2 TB WD SURVEILANCE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 212),
('HDD0028', 'M01', 'GOL01', 'A01', 'HDD 500GB  FOR NB SEAGATE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 213),
('HDD0052K', 'M01', 'GOL01', 'A01', 'HDD ADD13-500GU3-CSV 500GB ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 214),
('HDD0054K', 'M01', 'GOL01', 'A01', 'HDD AHD650-HU3-CRD 1TB ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 215),
('HDD0051K', 'M01', 'GOL01', 'A01', 'HDD AHH13-500GU3-CBK 500GB  ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 216),
('HDD0053K', 'M01', 'GOL01', 'A01', 'HDD AHV611-HU3-CWH 1TB ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 217),
('HDD0035', 'M01', 'GOL01', 'A01', 'HDD EXT 1TB SG EXPANSION', '-', '-', '-', 0, 0, 0, 825000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 218),
('HDD0039', 'M01', 'GOL01', 'A01', 'HDD EXT SEAGATE 500GB EXPANSION', '-', '-', '-', 0, 0, 0, 710000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 219),
('HDD0034', 'M01', 'GOL01', 'A01', 'HDD EXT WD ELEMENTS 500GB USB 3.0', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 220),
('HDD0036', 'M01', 'GOL01', 'A01', 'HDD EXT WD ULTRA 1 TB USB 3.0', '-', '-', '-', 0, 0, 0, 970000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 221),
('HDD0011', 'M01', 'GOL01', 'A01', 'HDD EXTERNAL 160GB + CASE 2,5 SEAGATE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 222),
('HDD0044', 'M01', 'GOL01', 'A01', 'HDD FOR NB 320GB WDC', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 223),
('HDD0041', 'M01', 'GOL01', 'A01', 'HDD NB TOSHIBA  SATA 500GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224),
('HDD0061', 'M01', 'GOL01', 'A01', 'HDD NB TOSHIBA 1 TB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 225),
('HDD0058', 'M01', 'GOL01', 'A01', 'HDD NH13 500GB ADATA', '-', '-', '-', 0, 0, 0, 730000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 226),
('HDD0026', 'M01', 'GOL01', 'A01', 'HDD SATA 1TB SEAGATE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 227),
('HDD0029', 'M01', 'GOL01', 'A01', 'HDD SATA 2TB SEAGATE', '-', '-', '-', 0, 0, 0, 1005000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 228),
('HDD0019', 'M01', 'GOL01', 'A01', 'HDD SATA 320GB SEAGATE', '-', '-', '-', 0, 0, 0, 450000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 229),
('HDD0022', 'M01', 'GOL01', 'A01', 'HDD SATA 500GB SEAGATE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 230),
('HDD0050', 'M01', 'GOL01', 'A01', 'HDD SURVEILANCE 1TB SEAGATE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 231),
('HDD0063', 'M01', 'GOL01', 'A01', 'HDD SURVEILANCE 1TB WD', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 232),
('HDD0055', 'M01', 'GOL01', 'A01', 'HDD SURVEILANCE 2 TB SEAGATE', '-', '-', '-', 0, 0, 0, 1075000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 233),
('HDD0064', 'M01', 'GOL01', 'A01', 'HDD SURVEILANCE 4TB SEAGATE', '-', '-', '-', 0, 0, 0, 1980000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 234),
('HDD0062', 'M01', 'GOL01', 'A01', 'HDD SURVEILANCE 4TB WD', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 235),
('HDD0043', 'M01', 'GOL01', 'A01', 'HDD WDC 500GB SATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 236),
('HDD0046', 'M01', 'GOL01', 'A01', 'HDD WDC NB (2,5\") 500GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 237),
('HEA0039', 'M01', 'GOL01', 'A01', 'HEAD PRINTER EPSON LQ 2170/2180/2190', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 238),
('HEA0034', 'M01', 'GOL01', 'A01', 'HEADSET KEENION KDM10', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 239),
('HEA0016', 'M01', 'GOL01', 'A01', 'HEADSET KENNION X-15A BLUE', '-', '-', '-', 0, 0, 0, 67000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 240),
('HUB0001', 'M01', 'GOL01', 'A01', 'HUB POE 4 PORT', '-', '-', '-', 0, 0, 0, 675000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 241),
('KER0005', 'M01', 'GOL01', 'A01', 'HVS SIDU F4 60 G', '-', '-', '-', 0, 0, 0, 31500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 242),
('CAR0022', 'M01', 'GOL01', 'A01', 'I/O CARD/ CARD LPT/ PCI PARAREL CARD', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 243),
('INF0003', 'M01', 'GOL01', 'A01', 'INFUS CANON BOTOL KOSONG', '-', '-', '-', 0, 0, 0, 40000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 244),
('INF0002', 'M01', 'GOL01', 'A01', 'INFUS+ TINTA', '-', '-', '-', 0, 0, 0, 75000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 245),
('PRO0042', 'M01', 'GOL01', 'A01', 'INTEL CORE I3 GHZ/ 3220/4150', '-', '-', '-', 0, 0, 0, 1790000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 246),
('PRO0033', 'M01', 'GOL01', 'A01', 'INTEL CORE I5 3.2GHZ', '-', '-', '-', 0, 0, 0, 7150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 247),
('PRO0037', 'M01', 'GOL01', 'A01', 'INTEL CORE I7', '-', '-', '-', 0, 0, 0, 7700000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 248),
('PRO0036', 'M01', 'GOL01', 'A01', 'INTEL DC G2020 2.9 GHZ', '-', '-', '-', 0, 0, 0, 4250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 249),
('PRO0044', 'M01', 'GOL01', 'A01', 'INTEL DC G2030', '-', '-', '-', 0, 0, 0, 3050000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 250),
('PRO0026', 'M01', 'GOL01', 'A01', 'INTEL PENTIUM DC 3GHZ', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 251),
('ZI0001', 'M01', 'GOL01', 'A01', 'INTERNET 1MB', '-', '-', '-', 0, 0, 0, 350000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 252),
('CAM0043', 'M01', 'GOL01', 'A01', 'IP CAMERA TRI VIB38', '-', '-', '-', 0, 0, 0, 1150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 253),
('JAC0002', 'M01', 'GOL01', 'A01', 'JACK TV', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 254),
('KAB0029', 'M01', 'GOL01', 'A01', 'KABEL ALARM SC4702TCW SC6702TWC', '-', '-', '-', 0, 0, 0, 7000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255),
('KAB0044', 'M01', 'GOL01', 'A01', 'KABEL CCTV', '-', '-', '-', 0, 0, 0, 5000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 256),
('KAB0051', 'M01', 'GOL01', 'A01', 'KABEL HDMI 1,5M', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 257),
('KAB0056', 'M01', 'GOL01', 'A01', 'KABEL HDMI 10 M', '-', '-', '-', 0, 0, 0, 145000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 258),
('KAB0055', 'M01', 'GOL01', 'A01', 'KABEL HDMI 15 M', '-', '-', '-', 0, 0, 0, 250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 259),
('KAB0061', 'M01', 'GOL01', 'A01', 'KABEL HDMI 20M', '-', '-', '-', 0, 0, 0, 285000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 260),
('KAB0052', 'M01', 'GOL01', 'A01', 'KABEL HDMI 3 M', '-', '-', '-', 0, 0, 0, 75000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 261),
('KAB0054', 'M01', 'GOL01', 'A01', 'KABEL HDMI 5 M', '-', '-', '-', 0, 0, 0, 100000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 262),
('KAB0042', 'M01', 'GOL01', 'A01', 'KABEL HDMI TO VGA', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 263),
('KAB0047', 'M01', 'GOL01', 'A01', 'KABEL LISTRIK', '-', '-', '-', 0, 0, 0, 6500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 264),
('KAB0030', 'M01', 'GOL01', 'A01', 'KABEL LPT MB', '-', '-', '-', 0, 0, 0, 76000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 265),
('KAB0024', 'M01', 'GOL01', 'A01', 'KABEL PERPANJANGAN USB 5M', '-', '-', '-', 0, 0, 0, 55000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 266),
('KAB0002', 'M01', 'GOL01', 'A01', 'KABEL POWER', '-', '-', '-', 0, 0, 0, 30000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 267),
('KAB0050', 'M01', 'GOL01', 'A01', 'KABEL POWER NOTEBOOK 3L', '-', '-', '-', 0, 0, 0, 30000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 268),
('KAB0004', 'M01', 'GOL01', 'A01', 'KABEL PRINTER PARALEL 10M', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 269),
('KAB0022', 'M01', 'GOL01', 'A01', 'KABEL PRINTER PARALEL 3 M', '-', '-', '-', 0, 0, 0, 45000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 270),
('KAB0019', 'M01', 'GOL01', 'A01', 'KABEL PRINTER PARALEL 5M', '-', '-', '-', 0, 0, 0, 75000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 271),
('KAB0005', 'M01', 'GOL01', 'A01', 'KABEL PRINTER PARALEL STANDARD', '-', '-', '-', 0, 0, 0, 35000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 272),
('KAB0017', 'M01', 'GOL01', 'A01', 'KABEL S-ATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 273),
('KAB0008', 'M01', 'GOL01', 'A01', 'KABEL SAMBUNGAN POWER MONITOR CPU', '-', '-', '-', 0, 0, 0, 25000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 274),
('KAB0058', 'M01', 'GOL01', 'A01', 'KABEL SAMBUNGAN USB 10M', '-', '-', '-', 0, 0, 0, 75000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 275),
('KAB0035', 'M01', 'GOL01', 'A01', 'KABEL SAMBUNGAN USB 3M', '-', '-', '-', 0, 0, 0, 40000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 276),
('KAB0028', 'M01', 'GOL01', 'A01', 'KABEL SAMBUNGAN USB 5M', '-', '-', '-', 0, 0, 0, 35000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 277),
('KAB0009', 'M01', 'GOL01', 'A01', 'KABEL SAMBUNGAN USB.', '-', '-', '-', 0, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 278),
('KAB0039', 'M01', 'GOL01', 'A01', 'KABEL TELPON', '-', '-', '-', 0, 0, 0, 4000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 279),
('KAB0059', 'M01', 'GOL01', 'A01', 'KABEL TELPON 10', '-', '-', '-', 0, 0, 0, 7500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 280),
('KAB0043', 'M01', 'GOL01', 'A01', 'KABEL TELPON 2C', '-', '-', '-', 0, 0, 0, 215000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 281),
('KAB0031', 'M01', 'GOL01', 'A01', 'KABEL TESTER', '-', '-', '-', 0, 0, 0, 38000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 282),
('KAB0027', 'M01', 'GOL01', 'A01', 'KABEL USB DATA 5 PIN', '-', '-', '-', 0, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 283),
('KAB0037', 'M01', 'GOL01', 'A01', 'KABEL USB PRINTER 10MTR', '-', '-', '-', 0, 0, 0, 75000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 284),
('KAB0036', 'M01', 'GOL01', 'A01', 'KABEL USB PRINTER 3M', '-', '-', '-', 0, 0, 0, 40000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 285),
('KAB0041', 'M01', 'GOL01', 'A01', 'KABEL USB PRINTER 5MTR', '-', '-', '-', 0, 0, 0, 55000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 286),
('KAB0012', 'M01', 'GOL01', 'A01', 'KABEL USB PRINTER.', '-', '-', '-', 0, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 287),
('KAB0018', 'M01', 'GOL01', 'A01', 'KABEL USB TO PARALEL', '-', '-', '-', 0, 0, 0, 175000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 288),
('KAB0021', 'M01', 'GOL01', 'A01', 'KABEL USB TO USB', '-', '-', '-', 0, 0, 0, 25000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 289),
('KAB0034', 'M01', 'GOL01', 'A01', 'KABEL UTP BELDEN /M.', '-', '-', '-', 0, 0, 0, 5000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 290),
('KAB0014', 'M01', 'GOL01', 'A01', 'KABEL UTP BELDEN.', '-', '-', '-', 0, 0, 0, 2500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 291),
('KAB0062', 'M01', 'GOL01', 'A01', 'KABEL UTP CAT5', '-', '-', '-', 0, 0, 0, 2500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 292),
('KAB0064', 'M01', 'GOL01', 'A01', 'KABEL UTP CAT6 SECURE', 'METER', '', '-', 100, 0, 20000, 10000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 293),
('KAB0046', 'M01', 'GOL01', 'A01', 'KABEL VGA 1.5M/2M', '-', '-', '-', 0, 0, 0, 30000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 294),
('KAB0038', 'M01', 'GOL01', 'A01', 'KABEL VGA 10 M', '-', '-', '-', 0, 0, 0, 120000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 295),
('KAB0063', 'M01', 'GOL01', 'A01', 'KABEL VGA 15 M', '-', '-', '-', 0, 0, 0, 235000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 296),
('KAB0032', 'M01', 'GOL01', 'A01', 'KABEL VGA 1M', '-', '-', '-', 0, 0, 0, 30000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 297),
('KAB0045', 'M01', 'GOL01', 'A01', 'KABEL VGA 20M', '-', '-', '-', 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 298),
('KAB0060', 'M01', 'GOL01', 'A01', 'KABEL VGA 30M', '-', '-', '-', 0, 0, 0, 380000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 299),
('KAB0023', 'M01', 'GOL01', 'A01', 'KABEL VGA 3M', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 300),
('KAB0033', 'M01', 'GOL01', 'A01', 'KABEL VGA 5M', '-', '-', '-', 0, 0, 0, 70000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 301),
('KAR0003', 'M01', 'GOL01', 'A01', 'KARTU 3', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 302),
('KAR0001', 'M01', 'GOL01', 'A01', 'KARTU GARANSI', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 303),
('KEY0037', 'M01', 'GOL01', 'A01', 'KB MOUUSE WIRELESS COMBO 8000X', '-', '-', '-', 0, 0, 0, 190000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 304),
('KER0007', 'M01', 'GOL01', 'A01', 'KERTAS BON  UK1/2 F4 / 3PLY', '-', '-', '-', 0, 0, 0, 21000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 305),
('KER0008', 'M01', 'GOL01', 'A01', 'KERTAS FAKTUR 1/2 FOLIO 2 PLY', '-', '-', '-', 0, 0, 0, 18000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 306),
('KER0006', 'M01', 'GOL01', 'A01', 'KERTAS STENSIL A4', '-', '-', '-', 0, 0, 0, 30000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 307),
('KER0004', 'M01', 'GOL01', 'A01', 'KERTAS STENSIL F4', '-', '-', '-', 0, 0, 0, 28000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 308),
('KEY0040', 'M01', 'GOL01', 'A01', 'KEYBOARD + MOUSE LOGITECH MK200', '-', '-', '-', 0, 0, 0, 240000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 309),
('KEY0039', 'M01', 'GOL01', 'A01', 'KEYBOARD ACER U/NB 453/4739', '-', '-', '-', 0, 0, 0, 195000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 310),
('KEY0012', 'M01', 'GOL01', 'A01', 'KEYBOARD GENIUS USB 110X/K125', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 311),
('KEY0001', 'M01', 'GOL01', 'A01', 'KEYBOARD LOGITECH K100', '-', '-', '-', 0, 0, 0, 120000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 312),
('KEY0028', 'M01', 'GOL01', 'A01', 'KEYBOARD LOGITECH K120USB', '-', '-', '-', 0, 0, 0, 90000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 313),
('KEY0041', 'M01', 'GOL01', 'A01', 'KEYBOARD MOUSE GENIUS KM200', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 314),
('KEY0030', 'M01', 'GOL01', 'A01', 'KEYBOARD MOUSE GIGABYTE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 315),
('KEY0044', 'M01', 'GOL01', 'A01', 'KEYBOARD NB (ASUS/TOSHIBA DLL)', '-', '-', '-', 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 316),
('KEY0042', 'M01', 'GOL01', 'A01', 'KEYBOARD POWER', '-', '-', '-', 0, 0, 0, 60000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 317),
('KEY0035', 'M01', 'GOL01', 'A01', 'KEYBOARD PROLINK', '-', '-', '-', 0, 0, 0, 120000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 318),
('KEY0016', 'M01', 'GOL01', 'A01', 'KEYBOARD PROTECTOR', '-', '-', '-', 0, 0, 0, 5000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 319),
('KEY0029', 'M01', 'GOL01', 'A01', 'KEYBOARD WIRELESS LOGITECH', '-', '-', '-', 0, 0, 0, 225000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 320),
('KEY0045', 'M01', 'GOL01', 'A01', 'KEYBOARD WIRELESS SYNTEC K189', '-', '-', '-', 0, 0, 0, 185000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 321),
('KEY0007', 'M01', 'GOL01', 'A01', 'KEYBOARD+MOUSE GENIUS', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 322),
('KEY0027', 'M01', 'GOL01', 'A01', 'KEYBOARD+MOUSE GENIUS WIRELESS', '-', '-', '-', 0, 0, 0, 245000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 323),
('KEY0034', 'M01', 'GOL01', 'A01', 'KEYBOARD+MOUSE LOGITECH MK120', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 324),
('KEY0004', 'M01', 'GOL01', 'A01', 'KEYBOARD+MOUSE WIRELESS LOGITECH', 'SET', 'PCS', '', 2, 10, 300000, 260000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 325),
('KIC0001', 'M01', 'GOL01', 'A01', 'KICK B 36', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 326),
('KIP0004', 'M01', 'GOL01', 'A01', 'KIPAS CASING / CPU COOLER', '-', '-', '-', 0, 0, 0, 27000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 327),
('KIP0010', 'M01', 'GOL01', 'A01', 'KIPAS NB  1 FAN /EYOTA/POWER', '-', '-', '-', 0, 0, 0, 45000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 328),
('KIP0008', 'M01', 'GOL01', 'A01', 'KIPAS NB TRANSPARAN', '-', '-', '-', 0, 0, 0, 40000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 329),
('KIP0005', 'M01', 'GOL01', 'A01', 'KIPAS NB/COOLING PAD LIPAT/KEPITING', '-', '-', '-', 0, 0, 0, 25000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 330),
('KIP0006', 'M01', 'GOL01', 'A01', 'KIPAS NOTEBOOK/COOLING PAD 3 FAN', '-', '-', '-', 0, 0, 0, 40000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 331),
('KIP0002', 'M01', 'GOL01', 'A01', 'KIPAS PROCESSOR 775', '-', '-', '-', 0, 0, 0, 85000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 332),
('ZLAI0002', 'M01', 'GOL01', 'A01', 'KOMPONEN SERVICE /SERVICE LUAR', '-', '-', '-', 0, 0, 0, 250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 333),
('KUN0003', 'M01', 'GOL01', 'A01', 'KUNCI CONTROL', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 334),
('KUN0002', 'M01', 'GOL01', 'A01', 'KUNCI ELECTRIK LOCK', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 335),
('KUN0001', 'M01', 'GOL01', 'A01', 'KUNCI LAPTOP', '-', '-', '-', 0, 0, 0, 45000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 336),
('LAB0001', 'M01', 'GOL01', 'A01', 'LABEL BARCODE 23X30MM', '-', '-', '-', 0, 0, 0, 120000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 337),
('LAB0002', 'M01', 'GOL01', 'A01', 'LABEL THERMAL TIMBANGAN', '-', '-', '-', 0, 0, 0, 39000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 338),
('LAB0003', 'M01', 'GOL01', 'A01', 'LABEL TIMBANGAN', '-', '-', '-', 0, 0, 0, 45000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 339),
('ZLAINLAIN', 'M01', 'GOL01', 'A01', 'LAIN LAIN', '-', '-', '-', 0, 0, 0, 180000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 340),
('LAN0001', 'M01', 'GOL01', 'A01', 'LAN CARD DLINK', '-', '-', '-', 0, 0, 0, 175000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 341);
INSERT INTO `zstok` (`kodebrg`, `kodemerk`, `kodegol`, `kodegrup`, `namabrg`, `satuan1`, `satuan2`, `satuan3`, `isi1`, `isi2`, `hrgbeli`, `harga1`, `harga11`, `harga111`, `harga2`, `harga22`, `harga222`, `harga3`, `harga33`, `harga333`, `harga4`, `harga44`, `harga444`, `harga5`, `harga55`, `harga555`, `harga6`, `harga66`, `harga666`, `autoid`) VALUES
('LAN0002', 'M01', 'GOL01', 'A01', 'LAN CARD TP-LINK', '-', '-', '-', 0, 0, 0, 90000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 342),
('LAN0003', 'M01', 'GOL01', 'A01', 'LAN CARD TP-LINK PCI EXPRESS', '-', '-', '-', 0, 0, 0, 175000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 343),
('LCD0001', 'M01', 'GOL01', 'A01', 'LCD CLEANER', '-', '-', '-', 0, 0, 0, 10000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 344),
('LCD0002', 'M01', 'GOL01', 'A01', 'LCD LAPTOP (PENGGANTIAN)', '-', '-', '-', 0, 0, 0, 600000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 345),
('MON0017', 'M01', 'GOL01', 'A01', 'LCD TOUCH SCREEN AOC 15.6', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 346),
('MON0026', 'M01', 'GOL01', 'A01', 'LED LG TV 24MN42A-PT 24\"', '-', '-', '-', 0, 0, 0, 1700000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 347),
('MAG0001', 'M01', 'GOL01', 'A01', 'MAGNETIC DUTY CONTACT (P)', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 348),
('MAG0002', 'M01', 'GOL01', 'A01', 'MAGNETIC HEAVY CONTACT (BESI)', '-', '-', '-', 0, 0, 0, 150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 349),
('MAI0030', 'M01', 'GOL01', 'A01', 'MAINBOARD  ECS G31T/ SAVIO G31', '-', '-', '-', 0, 0, 0, 700000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 350),
('MAI0052', 'M01', 'GOL01', 'A01', 'MAINBOARD ASROCK G41MVS3R2 LGA (775)', '-', '-', '-', 0, 0, 0, 780000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 351),
('MAI0051', 'M01', 'GOL01', 'A01', 'MAINBOARD ASROCK H61M', '-', '-', '-', 0, 0, 0, 880000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 352),
('MAI0050', 'M01', 'GOL01', 'A01', 'MAINBOARD ASROCK H71M-DGS LGA(1155)', '-', '-', '-', 0, 0, 0, 605000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 353),
('MAI0061', 'M01', 'GOL01', 'A01', 'MAINBOARD ASUS A68HM K', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 354),
('MAI0057', 'M01', 'GOL01', 'A01', 'MAINBOARD ASUS H81', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 355),
('MAI0006', 'M01', 'GOL01', 'A01', 'MAINBOARD PRINTER LQ2180', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 356),
('MAI0062', 'M01', 'GOL01', 'A01', 'MB ASROCK AMD', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 357),
('MAI0055', 'M01', 'GOL01', 'A01', 'MB ASROCK H81', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 358),
('MAI0059', 'M01', 'GOL01', 'A01', 'MB ASUS H61M', '-', '-', '-', 0, 0, 0, 930000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 359),
('MAI0058', 'M01', 'GOL01', 'A01', 'MB ESONIC G41', '-', '-', '-', 0, 0, 0, 650000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 360),
('MAI0041', 'M01', 'GOL01', 'A01', 'MB FOXCONN H61MX', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 361),
('MAI0064', 'M01', 'GOL01', 'A01', 'MB GA-H110M DS2', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 362),
('MAI0046', 'M01', 'GOL01', 'A01', 'MB GA H61M-DS2', '-', '-', '-', 0, 0, 0, 980000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 363),
('MAI0063', 'M01', 'GOL01', 'A01', 'MB GIGABYTE B85M DS3H A', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 364),
('MAI0044', 'M01', 'GOL01', 'A01', 'MB GIGABYTE GA-H61M-S2P', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 365),
('MAI0056', 'M01', 'GOL01', 'A01', 'MB GIGABYTE H 81', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 366),
('MAI0060', 'M01', 'GOL01', 'A01', 'MB MSI H61M', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 367),
('MES0001', 'M01', 'GOL01', 'A01', 'MESIN ABSENSI SIDIK JARI', '-', '-', '-', 0, 0, 0, 1725000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 368),
('SIR0001', 'M01', 'GOL01', 'A01', 'METAL SIRENE  BOX', '-', '-', '-', 0, 0, 0, 800000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 369),
('MIC0016K', 'M01', 'GOL01', 'A01', 'MICRI SD SANDISK ULTRA 32GB C10 48MB/S', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 370),
('MIC0002', 'M01', 'GOL01', 'A01', 'MICRO M2 2GB V-GEN', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 371),
('MIC0005', 'M01', 'GOL01', 'A01', 'MICRO SD 16GB V-GEN', '-', '-', '-', 0, 0, 0, 85000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 372),
('MIC0009', 'M01', 'GOL01', 'A01', 'MICRO SD 32GB V-GEN', '-', '-', '-', 0, 0, 0, 110000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 373),
('MIC0008', 'M01', 'GOL01', 'A01', 'MICRO SD 4GB V-GEN', '-', '-', '-', 0, 0, 0, 30000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 374),
('MIC0007', 'M01', 'GOL01', 'A01', 'MICRO SD 8 GB V-GEN', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 375),
('MIC0013K', 'M01', 'GOL01', 'A01', 'MICRO SD SANDISK SDHC 16GB MOBILE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 376),
('MIC0011K', 'M01', 'GOL01', 'A01', 'MICRO SD SANDISK SDHC 4GB MOBILE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 377),
('MIC0012K', 'M01', 'GOL01', 'A01', 'MICRO SD SANDISK SDHC 8GB MOBILE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 378),
('MIC0015K', 'M01', 'GOL01', 'A01', 'MICRO SD SANDISK ULTRA 16GB C10 48MB/S', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 379),
('MIC0014K', 'M01', 'GOL01', 'A01', 'MICRO SD SANDISK ULTRA 8GB C10 48MB/S', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 380),
('MIC0018', 'M01', 'GOL01', 'A01', 'MICRO SD VGEN 128GB', '-', '-', '-', 0, 0, 0, 480000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 381),
('MIC0017', 'M01', 'GOL01', 'A01', 'MICRO SD VGEN 64GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 382),
('MIK0001', 'M01', 'GOL01', 'A01', 'MIKROTIK', '-', '-', '-', 0, 0, 0, 785000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 383),
('MIN0003', 'M01', 'GOL01', 'A01', 'MINI PC FUJITECH MPX2500', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 384),
('MIN0002', 'M01', 'GOL01', 'A01', 'MINI PC FUJITECH MPX3700', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 385),
('MIN0004', 'M01', 'GOL01', 'A01', 'MINI PC NUC', '-', '-', '-', 0, 0, 0, 3650000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 386),
('MIN0006', 'M01', 'GOL01', 'A01', 'MINI PC NUC CI 5', '-', '-', '-', 0, 0, 0, 9500000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 387),
('MIN0005', 'M01', 'GOL01', 'A01', 'MINI PC NUC CORE I3', '-', '-', '-', 0, 0, 0, 5550000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 388),
('MOD0045', 'M01', 'GOL01', 'A01', 'MODEM ADSL TENDA', '-', '-', '-', 0, 0, 0, 400000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 389),
('MOD0003', 'M01', 'GOL01', 'A01', 'MODEM ADSL TP LINK TD-8817', '-', '-', '-', 0, 0, 0, 180000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 390),
('MOD0004', 'M01', 'GOL01', 'A01', 'MODEM ADSL WIRELESS DLINK', '-', '-', '-', 0, 0, 0, 335000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 391),
('MOD0033', 'M01', 'GOL01', 'A01', 'MODEM ADSL+WIFI TP-LINK TD-8951/8961 ND', '-', '-', '-', 0, 0, 0, 485000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 392),
('MOD0042', 'M01', 'GOL01', 'A01', 'MODEM BOLT', '-', '-', '-', 0, 0, 0, 500000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 393),
('MOD0046', 'M01', 'GOL01', 'A01', 'MODEM GSM UNEED', '-', '-', '-', 0, 0, 0, 230000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 394),
('MOD0041K', 'M01', 'GOL01', 'A01', 'MODEM HUAWEI 3G E3531', '-', '-', '-', 0, 0, 0, 290000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 395),
('MOD0044', 'M01', 'GOL01', 'A01', 'MODEM HUAWEI E3531', '-', '-', '-', 0, 0, 0, 285000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 396),
('MOD0030', 'M01', 'GOL01', 'A01', 'MODEM INTERNAL 56K', '-', '-', '-', 0, 0, 0, 133000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 397),
('MOD0034', 'M01', 'GOL01', 'A01', 'MODEM MOBILE PROLINK', '-', '-', '-', 0, 0, 0, 300000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 398),
('MOD0043', 'M01', 'GOL01', 'A01', 'MODEM ROUTER WIFI 300MBPS', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 399),
('MOD0002', 'M01', 'GOL01', 'A01', 'MODEM WAVECOM M13066', '-', '-', '-', 0, 0, 0, 4700000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 400),
('MON0021', 'M01', 'GOL01', 'A01', 'MONITOR LCD 16~SAMSUNG', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 401),
('MON0006', 'M01', 'GOL01', 'A01', 'MONITOR LED 15,6`` LG WDS', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 402),
('MON0010', 'M01', 'GOL01', 'A01', 'MONITOR LED AOC 16~~', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 403),
('MON0028', 'M01', 'GOL01', 'A01', 'MONITOR LG LED 20~', '-', '-', '-', 0, 0, 0, 1150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 404),
('MOU0043K', 'M01', 'GOL01', 'A01', 'MOUSE GAMING QANT MD', '-', '-', '-', 0, 0, 0, 110000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 405),
('MOU0044K', 'M01', 'GOL01', 'A01', 'MOUSE GAMING QANT WD', '-', '-', '-', 0, 0, 0, 135000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 406),
('MOU0045K', 'M01', 'GOL01', 'A01', 'MOUSE GAMING QANT WW', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 407),
('MOU0012', 'M01', 'GOL01', 'A01', 'MOUSE GENIUS  USB 110X SCROLL/120', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 408),
('MOU0024', 'M01', 'GOL01', 'A01', 'MOUSE OPTIC BLG', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 409),
('MOU0038', 'M01', 'GOL01', 'A01', 'MOUSE OPTIC LOGITECH  WIRELESS', '-', '-', '-', 0, 0, 0, 185000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 410),
('MOU0033', 'M01', 'GOL01', 'A01', 'MOUSE OPTIC SOTTA', '-', '-', '-', 0, 0, 0, 11000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 411),
('MOU0042', 'M01', 'GOL01', 'A01', 'MOUSE OPTICAL USB BYTECC', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 412),
('MOU0034', 'M01', 'GOL01', 'A01', 'MOUSE PAD ANGRY BIRD', '-', '-', '-', 0, 0, 0, 15000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 413),
('MOU0046', 'M01', 'GOL01', 'A01', 'MOUSE PAD BANTAL SUPER', '-', '-', '-', 0, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 414),
('MOU0035', 'M01', 'GOL01', 'A01', 'MOUSE PAD JEBER JM036A', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 415),
('MOU0007', 'M01', 'GOL01', 'A01', 'MOUSE PAD JEL/POLOS', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 416),
('MOU0048', 'M01', 'GOL01', 'A01', 'MOUSE USB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 417),
('MOU0047', 'M01', 'GOL01', 'A01', 'MOUSE USB LOGITECH', '-', '-', '-', 0, 0, 0, 70000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 418),
('MOU0040', 'M01', 'GOL01', 'A01', 'MOUSE USB POWER', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 419),
('MOU0014', 'M01', 'GOL01', 'A01', 'MOUSE WIRELESS GENIUS/ASUS/ACER', '-', '-', '-', 0, 0, 0, 85000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 420),
('MOU0049', 'M01', 'GOL01', 'A01', 'MOUSE WIRELESS PROLINK', '-', '-', '-', 0, 0, 0, 140000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 421),
('MOU0039', 'M01', 'GOL01', 'A01', 'MOUSEPAD DOLAR', '-', '-', '-', 0, 0, 0, 25000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 422),
('MP0002', 'M01', 'GOL01', 'A01', 'MP 3', '-', '-', '-', 0, 0, 0, 60000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 423),
('WIR0013', 'M01', 'GOL01', 'A01', 'N150MULTI-FUNCTION WIFI ROUTER EDIMAX', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 424),
('NAN0003', 'M01', 'GOL01', 'A01', 'NANO LITEBEAM M5', '-', '-', '-', 0, 0, 0, 1075000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 425),
('NAN0005', 'M01', 'GOL01', 'A01', 'NANO POWER BEAM M5 400 25DBI', '-', '-', '-', 0, 0, 0, 2000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 426),
('NAN0001', 'M01', 'GOL01', 'A01', 'NANO STATION LOCO M5', '-', '-', '-', 0, 0, 0, 1225000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 427),
('NAN0004', 'M01', 'GOL01', 'A01', 'NANO UNIFI LONG RANGE', '-', '-', '-', 0, 0, 0, 1650000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 428),
('NOT0233', 'M01', 'GOL01', 'A01', 'NB ACER 14\"', '-', '-', '-', 0, 0, 0, 4450000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 429),
('NOT0229', 'M01', 'GOL01', 'A01', 'NB ACER ASPIRE E5-421', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 430),
('NOT0231', 'M01', 'GOL01', 'A01', 'NB ACER E5-471', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 431),
('NOT0807', 'M01', 'GOL01', 'A01', 'NB ASUS 13\"', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 432),
('NOT0809', 'M01', 'GOL01', 'A01', 'NB ASUS 14\"', '-', '-', '-', 0, 0, 0, 7200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 433),
('NOT0810', 'M01', 'GOL01', 'A01', 'NB ASUS 15,6 / ALL IN ONE PC ASUS', '-', '-', '-', 0, 0, 0, 6499000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 434),
('NOT0812', 'M01', 'GOL01', 'A01', 'NB ASUS X200MA 11,6`', '-', '-', '-', 0, 0, 0, 2900000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 435),
('NOT0805', 'M01', 'GOL01', 'A01', 'NB ASUS X452EA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 436),
('NOT0806', 'M01', 'GOL01', 'A01', 'NB ASUS X455L', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 437),
('NOT0808', 'M01', 'GOL01', 'A01', 'NB ASUS X455LA-WX080D', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 438),
('NOT0609', 'M01', 'GOL01', 'A01', 'NB AXIOO NEON TNHC525X', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 439),
('NOT0501', 'M01', 'GOL01', 'A01', 'NB DELL', '-', '-', '-', 0, 0, 0, 5650000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 440),
('NOT0704', 'M01', 'GOL01', 'A01', 'NB LENOVO 11``', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 441),
('NET0002', 'M01', 'GOL01', 'A01', 'NETBOOK SKY EP1210', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 442),
('NOT0813', 'M01', 'GOL01', 'A01', 'NOTE BOOK ACER', '-', '-', '-', 0, 0, 0, 4000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 443),
('NOT0219', 'M01', 'GOL01', 'A01', 'NOTEBOOK ACER 11.6\"', '-', '-', '-', 0, 0, 0, 3300000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 444),
('NOT0302', 'M01', 'GOL01', 'A01', 'NOTEBOOK HP 14`', '-', '-', '-', 0, 0, 0, 6650000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 445),
('NOT0703', 'M01', 'GOL01', 'A01', 'NOTEBOOK LENOVO 14\"', '-', '-', '-', 0, 0, 0, 3500000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 446),
('SPE0030', 'M01', 'GOL01', 'A01', 'ONE PE DZ 210', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 447),
('ZONG0002', 'M01', 'GOL01', 'A01', 'ONGKOS KIRIM', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 448),
('ZALA0005', 'M01', 'GOL01', 'A01', 'ONLINE CCTV', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 449),
('POW0022K', 'M01', 'GOL01', 'A01', 'P SUPLY RAIDMAX RRX 380K', '-', '-', '-', 0, 0, 0, 295000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 450),
('PAP0005', 'M01', 'GOL01', 'A01', 'PAPER GLOSSY A6 200 GR/ A4', '-', '-', '-', 0, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 451),
('PAP0008', 'M01', 'GOL01', 'A01', 'PAPER ROLL  75 X 65 3PLY', 'DUS', '', '-', 10, 0, 110000, 5400, 7000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 452),
('PAP0006', 'M01', 'GOL01', 'A01', 'PAPER ROLL 75 X 65 2PLY', '-', '-', '-', 0, 0, 0, 4500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 453),
('PAP0002', 'M01', 'GOL01', 'A01', 'PAPER ROLL 80X80 THERMAL', '-', '-', '-', 0, 0, 0, 13000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 454),
('PAP0007', 'M01', 'GOL01', 'A01', 'PAPER ROLL TELSTRUK 58X65 FOR ECR XEA102', '-', '-', '-', 0, 0, 0, 2500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 455),
('PAP0010', 'M01', 'GOL01', 'A01', 'PAPER ROLL THERMAL 57X35', 'PCS', '-', '-', 1, 0, 3400, 3300, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 456),
('PAP0001', 'M01', 'GOL01', 'A01', 'PAPER ROLL/TELSTROOK 75X65 HVS', '-', '-', '-', 0, 0, 0, 3550, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 457),
('PAP0009', 'M01', 'GOL01', 'A01', 'PAPER THERMAL 57X46', '-', '-', '-', 0, 0, 0, 3800, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 458),
('PEN0001', 'M01', 'GOL01', 'A01', 'PENYAMBUNG LAN', '-', '-', '-', 0, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 459),
('PER0001', 'M01', 'GOL01', 'A01', 'PERFORATED BAG', '-', '-', '-', 0, 0, 0, 85000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 460),
('PIT0008', 'M01', 'GOL01', 'A01', 'PITA  7755 PRINTECH', '-', '-', '-', 0, 0, 0, 12000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 461),
('PIT0001', 'M01', 'GOL01', 'A01', 'PITA 7755 FULLMARK LX-300+', '-', '-', '-', 0, 0, 0, 14000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 462),
('PIT0009', 'M01', 'GOL01', 'A01', 'PITA ERC 38 (REFILL)', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 463),
('PIT0002', 'M01', 'GOL01', 'A01', 'PITA ERC 38P', '-', '-', '-', 0, 0, 0, 25000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 464),
('PIT0003', 'M01', 'GOL01', 'A01', 'PITA ERC 39P', '-', '-', '-', 0, 0, 0, 35000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 465),
('PIT0005', 'M01', 'GOL01', 'A01', 'PITA ORIGINAL UTK LQ2180/90', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 466),
('PIT0007', 'M01', 'GOL01', 'A01', 'PITA PRINTER LQ-2170 PRINTECH', '-', '-', '-', 0, 0, 0, 35000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 467),
('POL0001', 'M01', 'GOL01', 'A01', 'POLE DISPLAY LD202', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 468),
('PRI0089', 'M01', 'GOL01', 'A01', 'POS PRINTER MINI BLUETOOTH THERMAL 58A', '-', '-', '-', 0, 0, 0, 1180000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 469),
('PRI0001', 'M01', 'GOL01', 'A01', 'POS PRINTER THERMAL /TM 28 EPSON', '-', '-', '-', 0, 0, 0, 2900000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 470),
('POW0019', 'M01', 'GOL01', 'A01', 'POWER SUPLY DVR + BOX 5A', '-', '-', '-', 0, 0, 0, 300000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 471),
('POW0021', 'M01', 'GOL01', 'A01', 'POWER SUPPLY  CAM 10A (NON BOX)', '-', '-', '-', 0, 0, 0, 300000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 472),
('POW0029', 'M01', 'GOL01', 'A01', 'POWER SUPPLY  DVR 5A NON BOX', '-', '-', '-', 0, 0, 0, 150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 473),
('POW0020', 'M01', 'GOL01', 'A01', 'POWER SUPPLY 20A (NON BOX)', '-', '-', '-', 0, 0, 0, 290000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 474),
('POW0024', 'M01', 'GOL01', 'A01', 'POWER SUPPLY CABUTAN', '-', '-', '-', 0, 0, 0, 170000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 475),
('POW0018', 'M01', 'GOL01', 'A01', 'POWER SUPPLY DVR + BOX 10A', '-', '-', '-', 0, 0, 0, 385000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 476),
('POW0017', 'M01', 'GOL01', 'A01', 'POWER SUPPLY DVR 20A + BOX', '-', '-', '-', 0, 0, 0, 475000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 477),
('POW0028', 'M01', 'GOL01', 'A01', 'POWER SUPPLY NON BOX 30 A', '-', '-', '-', 0, 0, 0, 325000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 478),
('POW0002', 'M01', 'GOL01', 'A01', 'POWER SUPPLY POWERPRO', '-', '-', '-', 0, 0, 0, 150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 479),
('POW0023K', 'M01', 'GOL01', 'A01', 'POWER SUPPLY RAIDMAX RRX450', '-', '-', '-', 0, 0, 0, 410000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 480),
('POW0023', 'M01', 'GOL01', 'A01', 'POWER SUPPLY RAIDMAX RX-450K', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 481),
('PRI0088', 'M01', 'GOL01', 'A01', 'PRINT HEAD EPSON  LQ 2180', '-', '-', '-', 0, 0, 0, 1750000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 482),
('PRI0069', 'M01', 'GOL01', 'A01', 'PRINT HEAD TSC 244 PLUS', '-', '-', '-', 0, 0, 0, 2200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 483),
('PRI0078', 'M01', 'GOL01', 'A01', 'PRINT SERVER USB EDIMAX', '-', '-', '-', 0, 0, 0, 668000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 484),
('PRI0047', 'M01', 'GOL01', 'A01', 'PRINTER BARCODE TSC', '-', '-', '-', 0, 0, 0, 3800000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 485),
('PRI0081', 'M01', 'GOL01', 'A01', 'PRINTER BROTHER HL-1110 LASER', '-', '-', '-', 0, 0, 0, 850000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 486),
('PRI0092', 'M01', 'GOL01', 'A01', 'PRINTER BROTHER MFC-J3720', '-', '-', '-', 0, 0, 0, 6520000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 487),
('PRI0065', 'M01', 'GOL01', 'A01', 'PRINTER CANON MG 2570', '-', '-', '-', 0, 0, 0, 750000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 488),
('PRI0053', 'M01', 'GOL01', 'A01', 'PRINTER CANON MULTIFUNGSI MP-287', '-', '-', '-', 0, 0, 0, 950000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 489),
('PRI0073', 'M01', 'GOL01', 'A01', 'PRINTER CANON MX397', '-', '-', '-', 0, 0, 0, 1250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 490),
('PRI0030', 'M01', 'GOL01', 'A01', 'PRINTER CANON PIXMA IP2770', '-', '-', '-', 0, 0, 0, 800000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 491),
('PRI0057', 'M01', 'GOL01', 'A01', 'PRINTER CANON PIXMA MP237', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 492),
('PRI0063', 'M01', 'GOL01', 'A01', 'PRINTER CARD FARGO DTC1000/DTC 1250', '-', '-', '-', 0, 0, 0, 13900000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 493),
('PRI0058', 'M01', 'GOL01', 'A01', 'PRINTER EPSON L-210 MULTIFUNGSI', '-', '-', '-', 0, 0, 0, 2200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 494),
('PRI0079', 'M01', 'GOL01', 'A01', 'PRINTER EPSON L120', '-', '-', '-', 0, 0, 0, 1675000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 495),
('PRI0085', 'M01', 'GOL01', 'A01', 'PRINTER EPSON L1300', '-', '-', '-', 0, 0, 0, 5900000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 496),
('PRI0084', 'M01', 'GOL01', 'A01', 'PRINTER EPSON L220', '-', '-', '-', 0, 0, 0, 2400000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 497),
('PRI0072', 'M01', 'GOL01', 'A01', 'PRINTER EPSON L350/L360', '-', '-', '-', 0, 0, 0, 2500000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 498),
('PRI0068', 'M01', 'GOL01', 'A01', 'PRINTER EPSON L355/365', '-', '-', '-', 0, 0, 0, 2925000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 499),
('PRI0061', 'M01', 'GOL01', 'A01', 'PRINTER EPSON LX-310', '-', '-', '-', 0, 0, 0, 2200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 500),
('PRI0009', 'M01', 'GOL01', 'A01', 'PRINTER EPSON PLQ20 PASBOOK', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 501),
('PRI0024', 'M01', 'GOL01', 'A01', 'PRINTER EPSON TM-U220PA LPT/220PB USB', '-', '-', '-', 0, 0, 0, 2900000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 502),
('PRI0080', 'M01', 'GOL01', 'A01', 'PRINTER HP DESKJET 1010', '-', '-', '-', 0, 0, 0, 535000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 503),
('PRI0038', 'M01', 'GOL01', 'A01', 'PRINTER HP LASERJET 1102', '-', '-', '-', 0, 0, 0, 1310000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 504),
('PRI0091', 'M01', 'GOL01', 'A01', 'PRINTER HP LASERJET M102A', '-', '-', '-', 0, 0, 0, 1435000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 505),
('PRI0086', 'M01', 'GOL01', 'A01', 'PRINTER HP LASERJET M125', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 506),
('PRI0044', 'M01', 'GOL01', 'A01', 'PRINTER ID CARD ZEBRA', '-', '-', '-', 0, 0, 0, 12500000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 507),
('PRI0082', 'M01', 'GOL01', 'A01', 'PRINTER L555', '-', '-', '-', 0, 0, 0, 4000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 508),
('PRI0077', 'M01', 'GOL01', 'A01', 'PRINTER LASER HP 1102', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 509),
('PRI0067', 'M01', 'GOL01', 'A01', 'PRINTER LQ 310', '-', '-', '-', 0, 0, 0, 2550000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 510),
('PRI0025', 'M01', 'GOL01', 'A01', 'PRINTER MINI THERMAL', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 511),
('PRI0090', 'M01', 'GOL01', 'A01', 'PRINTER THERMAL MINI 5890K', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 512),
('PRI0003', 'M01', 'GOL01', 'A01', 'PRINTER TMU 220PD', '-', '-', '-', 0, 0, 0, 2550000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 513),
('PRI0087', 'M01', 'GOL01', 'A01', 'PRINTER TONER HP 16A', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 514),
('PRO0049', 'M01', 'GOL01', 'A01', 'PROC HASSWELL CI5', '-', '-', '-', 0, 0, 0, 7000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 515),
('PRO0048', 'M01', 'GOL01', 'A01', 'PROC HASSWELL CORE I3', '-', '-', '-', 0, 0, 0, 4400000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 516),
('PRO0047', 'M01', 'GOL01', 'A01', 'PROC HASSWELL DUAL CORE', '-', '-', '-', 0, 0, 0, 4200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 517),
('PRO0041', 'M01', 'GOL01', 'A01', 'PROCESSOR AMD A10', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 518),
('ZZZ', 'M01', 'GOL01', 'A01', 'PROGRAM', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 519),
('PRO0046', 'M01', 'GOL01', 'A01', 'PROSESOR AMD  A6/A4', '-', '-', '-', 0, 0, 0, 4250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 520),
('PRO0050', 'M01', 'GOL01', 'A01', 'PROYEKTOR  LCD ACER', '-', '-', '-', 0, 0, 0, 4000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 521),
('ZALA0004', 'M01', 'GOL01', 'A01', 'RAK TV', '-', '-', '-', 0, 0, 0, 120000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 522),
('RAM0016', 'M01', 'GOL01', 'A01', 'RAM DDR2 2GB PC-5300 V-GEN', '-', '-', '-', 0, 0, 0, 300000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 523),
('RAM0018', 'M01', 'GOL01', 'A01', 'RAM DDR2 2GB PC-6400 V-GEN', '-', '-', '-', 0, 0, 0, 450000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 524),
('RAM0028', 'M01', 'GOL01', 'A01', 'RAM DDR3 2GB  V-GEN', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 525),
('RAM0041', 'M01', 'GOL01', 'A01', 'RAM DDR3 2GB ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 526),
('RAM0030', 'M01', 'GOL01', 'A01', 'RAM DDR3 2GB PC-10600 VISIPRO', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 527),
('RAM0047', 'M01', 'GOL01', 'A01', 'RAM DDR3 4GB ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 528),
('RAM0033', 'M01', 'GOL01', 'A01', 'RAM DDR3 4GB PC-10600 V-GEN', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 529),
('RAM0035', 'M01', 'GOL01', 'A01', 'RAM DDR3 4GB VISIPRO', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 530),
('RAM0044', 'M01', 'GOL01', 'A01', 'RAM DDR3 8GB ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 531),
('RAM0036', 'M01', 'GOL01', 'A01', 'RAM DDR3 8GB PC-10600 V-GEN', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 532),
('RAM0038', 'M01', 'GOL01', 'A01', 'RAM DDR3 8GB VISIPRO', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 533),
('REF0001', 'M01', 'GOL01', 'A01', 'REFIL KIT E-PRINT B', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 534),
('REF0002', 'M01', 'GOL01', 'A01', 'REFIL TONER', '-', '-', '-', 0, 0, 0, 150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 535),
('TIN0038', 'M01', 'GOL01', 'A01', 'REFILL BLACK EPSON BLUEPRINT', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 536),
('TIN0051', 'M01', 'GOL01', 'A01', 'REFILL BOX PRINTECH EPSON BLACK', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 537),
('TIN0050', 'M01', 'GOL01', 'A01', 'REFILL BOX PRINTECH EPSON COLOR', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 538),
('TIN0048', 'M01', 'GOL01', 'A01', 'REFILL BOX PRINTECH HP BLACK', '-', '-', '-', 0, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 539),
('TIN0049', 'M01', 'GOL01', 'A01', 'REFILL BOX PRINTECH HP COLOR', '-', '-', '-', 0, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 540),
('TIN0037', 'M01', 'GOL01', 'A01', 'REFILL HP BLUEPRINT 4 COLOURS', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 541),
('TIN0043', 'M01', 'GOL01', 'A01', 'REFILL PACK HP COLOUR BP', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 542),
('RIB0001', 'M01', 'GOL01', 'A01', 'RIBBON BARCODE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 543),
('RIB0004', 'M01', 'GOL01', 'A01', 'RIBBON COLOR FARGO DTC1000/DTC 1250', '-', '-', '-', 0, 0, 0, 625000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 544),
('RIB0005', 'M01', 'GOL01', 'A01', 'RIBBON FARGO BLACK', '-', '-', '-', 0, 0, 0, 320000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 545),
('ROU0012', 'M01', 'GOL01', 'A01', 'ROUTER  WIRELESS TP LINK  WR740', '-', '-', '-', 0, 0, 0, 350000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 546),
('ROU0003', 'M01', 'GOL01', 'A01', 'ROUTER CISCOM + AP', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 547),
('ROU0008', 'M01', 'GOL01', 'A01', 'ROUTER LINKSYS E1200 4PORT', '-', '-', '-', 0, 0, 0, 600000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 548),
('ROU0014', 'M01', 'GOL01', 'A01', 'ROUTER WIR TP LINK 300MBPS 2ANT WR841HP', '-', '-', '-', 0, 0, 0, 550000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 549),
('ROU0007', 'M01', 'GOL01', 'A01', 'ROUTER WIRELESS TP-LINK  MR3420', '-', '-', '-', 0, 0, 0, 400000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 550),
('ROU0013', 'M01', 'GOL01', 'A01', 'ROUTER WIRELESS TP LINK WR 840N', '-', '-', '-', 0, 0, 0, 380000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 551),
('FLA0036', 'M01', 'GOL01', 'A01', 'SANDISK ULTRA DUAL DRIVE  USB 3.0 16GB', '-', '-', '-', 0, 0, 0, 150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 552),
('FLA0036K', 'M01', 'GOL01', 'A01', 'SANDISK ULTRA DUAL USB DRIVE 16GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 553),
('FLA0043K', 'M01', 'GOL01', 'A01', 'SANDISK ULTRA DUAL USB DRIVE 32GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 554),
('SCA0010', 'M01', 'GOL01', 'A01', 'SCANNER CANON LIDE 110/120', '-', '-', '-', 0, 0, 0, 835000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 555),
('SCA0004', 'M01', 'GOL01', 'A01', 'SCANNER ORBIT MK7120', '-', '-', '-', 0, 0, 0, 3000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 556),
('SCA0011', 'M01', 'GOL01', 'A01', 'SCANNER TEMBAK', '-', '-', '-', 0, 0, 0, 750000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 557),
('SCR0002', 'M01', 'GOL01', 'A01', 'SCREEN PROTECTOR FOR NB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 558),
('SD0001', 'M01', 'GOL01', 'A01', 'SD CARD 4GB V-GEN', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 559),
('ZSUL01', 'M01', 'GOL01', 'A01', 'SERVICE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 560),
('ZHADI01', 'M01', 'GOL01', 'A01', 'SERVICE', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 561),
('SID0001', 'M01', 'GOL01', 'A01', 'SIDIK JARI ENTERPRISE 2000', '-', '-', '-', 0, 0, 0, 2850000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 562),
('SID0005', 'M01', 'GOL01', 'A01', 'SIDIK JARI KANA', '-', '-', '-', 0, 0, 0, 1900000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 563),
('SID0003', 'M01', 'GOL01', 'A01', 'SIDIK JARI MATRIX SERIES', '-', '-', '-', 0, 0, 0, 1800000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 564),
('SID0004', 'M01', 'GOL01', 'A01', 'SIDIK JARI SOLUTION X100C', '-', '-', '-', 0, 0, 0, 2000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 565),
('SID0006', 'M01', 'GOL01', 'A01', 'SIDIK JARI STEALTH SF 302', '-', '-', '-', 0, 0, 0, 1500000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 566),
('SID0002', 'M01', 'GOL01', 'A01', 'SIDIK JARI U ARE U', '-', '-', '-', 0, 0, 0, 1550000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 567),
('RAM0029', 'M01', 'GOL01', 'A01', 'SODIMM  DDR3 2GB PC-10600 V-GEN', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 568),
('RAM0039', 'M01', 'GOL01', 'A01', 'SODIMM DDR3  4GB VISIPRO', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 569),
('RAM0042', 'M01', 'GOL01', 'A01', 'SODIMM DDR3 2GB ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 570),
('RAM0057', 'M01', 'GOL01', 'A01', 'SODIMM DDR3 2GB VENOM', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 571),
('RAM0043', 'M01', 'GOL01', 'A01', 'SODIMM DDR3 4GB ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 572),
('RAM0054', 'M01', 'GOL01', 'A01', 'SODIMM DDR3 4GB KINGSTON', '-', '-', '-', 0, 0, 0, 450000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 573),
('RAM0056', 'M01', 'GOL01', 'A01', 'SODIMM DDR3 4GB VENOM', '-', '-', '-', 0, 0, 0, 385000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 574),
('RAM0037', 'M01', 'GOL01', 'A01', 'SODIMM DDR3 4GB VGEN', '-', '-', '-', 0, 0, 0, 430000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 575),
('RAM0046', 'M01', 'GOL01', 'A01', 'SODIMM DDR3 8 GB ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 576),
('RAM0048', 'M01', 'GOL01', 'A01', 'SODIMM DDR4 4GB ADATA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 577),
('RAM0055', 'M01', 'GOL01', 'A01', 'SODIMM DDR4 4GB TEAM', '-', '-', '-', 0, 0, 0, 535000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 578),
('SOF0003', 'M01', 'GOL01', 'A01', 'SOFT CASE HDD EXTERNAL', '-', '-', '-', 0, 0, 0, 30000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 579),
('SPE0027', 'M01', 'GOL01', 'A01', 'SPEAKER ANGEL', '-', '-', '-', 0, 0, 0, 110000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 580),
('SPE0028', 'M01', 'GOL01', 'A01', 'SPEAKER ANGRY BIRD', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 581),
('SPE0036', 'M01', 'GOL01', 'A01', 'SPEAKER DAZUMBA DW 366', '-', '-', '-', 0, 0, 0, 575000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 582),
('SPE0037', 'M01', 'GOL01', 'A01', 'SPEAKER GMC BLUETOOTH', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 583),
('SPE0033', 'M01', 'GOL01', 'A01', 'SPEAKER MOZAIC 2.0 USB', '-', '-', '-', 0, 0, 0, 55000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 584),
('SPE0034', 'M01', 'GOL01', 'A01', 'SPEAKER SIMBADDA CST 1750N', '-', '-', '-', 0, 0, 0, 585000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 585),
('SPE0019', 'M01', 'GOL01', 'A01', 'SPEAKER SIMBADDA CST 6400N', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 586),
('SPE0035', 'M01', 'GOL01', 'A01', 'SPEAKER SOTTA', '-', '-', '-', 0, 0, 0, 75000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 587),
('SPL0001', 'M01', 'GOL01', 'A01', 'SPLITER VGA 1 TO 4 PORT', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 588),
('SPL0002', 'M01', 'GOL01', 'A01', 'SPLITTER HDMI 1-2 PORT', '-', '-', '-', 0, 0, 0, 125000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 589),
('HDD0049', 'M01', 'GOL01', 'A01', 'SSD ADATA 240GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 590),
('HDD0056', 'M01', 'GOL01', 'A01', 'SSD INTEL 240GB', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 591),
('HDD0048', 'M01', 'GOL01', 'A01', 'SSD INTEL 535 SERIES 120GB', 'PCS', '', '-', 10, 0, 1100000, 1200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 592),
('STA0003', 'M01', 'GOL01', 'A01', 'STABILIZER 1000VA', '-', '-', '-', 0, 0, 0, 310000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 593),
('STA0010', 'M01', 'GOL01', 'A01', 'STABILIZER VISALUX /POWELL 3000VA', '-', '-', '-', 0, 0, 0, 805000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 594),
('STI0003', 'M01', 'GOL01', 'A01', 'STICK PS DOUBLE-GETAR', '-', '-', '-', 0, 0, 0, 65000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 595),
('SUP0001', 'M01', 'GOL01', 'A01', 'SUPER GRAPHIC G210', '-', '-', '-', 0, 0, 0, 870000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 596),
('SWI0013', 'M01', 'GOL01', 'A01', 'SWITCH  MONITOR', '-', '-', '-', 0, 0, 0, 120000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 597),
('SWI0010', 'M01', 'GOL01', 'A01', 'SWITCH EDIMAX 5 PORT', '-', '-', '-', 0, 0, 0, 82000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 598),
('SWI0012', 'M01', 'GOL01', 'A01', 'SWITCH EDIMAX 8 PORT', '-', '-', '-', 0, 0, 0, 100000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 599),
('SWI0003', 'M01', 'GOL01', 'A01', 'SWITCH HUB 16 PORT TP-LINK', '-', '-', '-', 0, 0, 0, 425000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 600),
('SWI0001', 'M01', 'GOL01', 'A01', 'SWITCH HUB D-LINK 8 PORT', '-', '-', '-', 0, 0, 0, 150000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 601),
('SWI0011', 'M01', 'GOL01', 'A01', 'SWITCH HUB EDIMAX 5 PORT GIGABIT', '-', '-', '-', 0, 0, 0, 325000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 602),
('SWI0009', 'M01', 'GOL01', 'A01', 'SWITCH HUB TP-LINK 24PORT GIGABIT', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 603),
('SWI0005', 'M01', 'GOL01', 'A01', 'SWITCH HUB TP-LINK 5 PORT', '-', '-', '-', 0, 0, 0, 90000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 604),
('SWI0006', 'M01', 'GOL01', 'A01', 'SWITCH HUB TPLINK 8PORT', '-', '-', '-', 0, 0, 0, 145000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 605),
('SWI0008', 'M01', 'GOL01', 'A01', 'SWITCH TPLINK 8 PORT GIGABIT + RJ45 PORT', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 606),
('TAS0003', 'M01', 'GOL01', 'A01', 'TAS NOTEBOOK', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 607),
('TAS0004', 'M01', 'GOL01', 'A01', 'TAS NOTEBOOK  ACER', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 608),
('TEN0001', 'M01', 'GOL01', 'A01', 'TENDA SW HUB POE 1008P', '-', '-', '-', 0, 0, 0, 715000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 609),
('TIM0001', 'M01', 'GOL01', 'A01', 'TIMBANGAN ELEKTRONIK', '-', '-', '-', 0, 0, 0, 14000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 610),
('TIN0071', 'M01', 'GOL01', 'A01', 'TINTA BOTOL EPSON L-100 & L-200 B (T664)', '-', '-', '-', 0, 0, 0, 90000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 611),
('TIN0070', 'M01', 'GOL01', 'A01', 'TINTA BOTOL EPSON L-100 & L-200 C (T664)', '-', '-', '-', 0, 0, 0, 90000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 612),
('TIN0068', 'M01', 'GOL01', 'A01', 'TINTA BOTOL EPSON L-100 & L-200 M (T664)', '-', '-', '-', 0, 0, 0, 90000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 613),
('TIN0069', 'M01', 'GOL01', 'A01', 'TINTA BOTOL EPSON L-100 & L-200 Y (T664)', '-', '-', '-', 0, 0, 0, 90000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 614),
('TIN0013', 'M01', 'GOL01', 'A01', 'TINTA DURABRIGHT REFILL CANON BLACK', '-', '-', '-', 0, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 615),
('TIN0005', 'M01', 'GOL01', 'A01', 'TINTA DURABRIGHT REFILL CANON COLOR', '-', '-', '-', 0, 0, 0, 25000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 616),
('TIN0006', 'M01', 'GOL01', 'A01', 'TINTA DURABRIGHT REFILL EPSON BLACK', '-', '-', '-', 0, 0, 0, 25000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 617),
('TIN0008', 'M01', 'GOL01', 'A01', 'TINTA DURABRIGHT REFILL HP BLACK', '-', '-', '-', 0, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 618),
('TIN0009', 'M01', 'GOL01', 'A01', 'TINTA DURABRIGHT REFILL HP COLOR', '-', '-', '-', 0, 0, 0, 25000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 619),
('TIN0019', 'M01', 'GOL01', 'A01', 'TINTA DURABRITGHT BOTOL 200ML MAGENTA', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 620),
('TIN0085', 'M01', 'GOL01', 'A01', 'TINTA E-PRINT/ CANON 200ML B/C/M/Y', '-', '-', '-', 0, 0, 0, 45500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 621),
('TIN0083', 'M01', 'GOL01', 'A01', 'TINTA F1 CANON 200ML', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 622),
('TIN0082', 'M01', 'GOL01', 'A01', 'TINTA F1 CANON 70ML', '-', '-', '-', 0, 0, 0, 30000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 623),
('TIN0075', 'M01', 'GOL01', 'A01', 'TINTA REFILL BP CANON CYAN 250ML', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 624),
('TIN0076', 'M01', 'GOL01', 'A01', 'TINTA REFILL BP CANON MAGENTA 250 ML', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 625),
('TIN0077', 'M01', 'GOL01', 'A01', 'TINTA REFILL BP CANON YELLOW 250ML', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 626),
('TIN0084', 'M01', 'GOL01', 'A01', 'TINTA REFILL HP E-PRINT 100ML', '-', '-', '-', 0, 0, 0, 35000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 627),
('TON0009', 'M01', 'GOL01', 'A01', 'TONER BROTHER 1000', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 628),
('TON0008', 'M01', 'GOL01', 'A01', 'TONER HP 85A', '-', '-', '-', 0, 0, 0, 550000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 629),
('TV0001', 'M01', 'GOL01', 'A01', 'TV TUNER COMBO GADMEI', '-', '-', '-', 0, 0, 0, 355000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 630),
('UPS0003', 'M01', 'GOL01', 'A01', 'UPS ICA 1200VA 602B(BISA AKI EXTERNAL)', '-', '-', '-', 0, 0, 0, 2070000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 631),
('UPS0002', 'M01', 'GOL01', 'A01', 'UPS ICA 1200VA CT-682B', '-', '-', '-', 0, 0, 0, 1800000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 632),
('UPS0008', 'M01', 'GOL01', 'A01', 'UPS ICA 2000VA CT1082B', '-', '-', '-', 0, 0, 0, 2640000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 633),
('UPS0010', 'M01', 'GOL01', 'A01', 'UPS ICA CP700 700VA', '-', '-', '-', 0, 0, 0, 700000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 634),
('UPS0007', 'M01', 'GOL01', 'A01', 'UPS POWERPRO  600VA', '-', '-', '-', 0, 0, 0, 375000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 635),
('UPS0011', 'M01', 'GOL01', 'A01', 'UPS PROLINK 1200VA', '-', '-', '-', 0, 0, 0, 1000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 636),
('UPS0013', 'M01', 'GOL01', 'A01', 'UPS PROLINK 700VA', '-', '-', '-', 0, 0, 0, 595000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 637),
('UPS0020', 'M01', 'GOL01', 'A01', 'UPS SOCOMEC NPE -B600/650', '-', '-', '-', 0, 0, 0, 760000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 638),
('UPS0021', 'M01', 'GOL01', 'A01', 'UPS SOCOMEC NPE 1000LCD', '-', '-', '-', 0, 0, 0, 1550000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 639),
('UPS0022', 'M01', 'GOL01', 'A01', 'UPS SOCOMEC NPE 1500LCD', '-', '-', '-', 0, 0, 0, 1685000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 640),
('USB0004', 'M01', 'GOL01', 'A01', 'USB HUB 4PORT', '-', '-', '-', 0, 0, 0, 135000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 641),
('USB0007', 'M01', 'GOL01', 'A01', 'USB HUB 7 PORT', '-', '-', '-', 0, 0, 0, 90000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 642),
('USB0005', 'M01', 'GOL01', 'A01', 'USB LAN', '-', '-', '-', 0, 0, 0, 70000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 643),
('VAC0001', 'M01', 'GOL01', 'A01', 'VACUM CLEANER', '-', '-', '-', 0, 0, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 644),
('VGA0036', 'M01', 'GOL01', 'A01', 'VGA CARD POWER COLOR R7 240 2GB DDR5', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 645),
('VGA0027', 'M01', 'GOL01', 'A01', 'VGA GIGABYTE GF 220 1GB DDR3', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 646),
('VGA0021', 'M01', 'GOL01', 'A01', 'VGA GT-210 512MB', '-', '-', '-', 0, 0, 0, 550000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 647),
('VGA0035', 'M01', 'GOL01', 'A01', 'VGA HIS 250 2GB DDR5', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 648),
('VGA0033K', 'M01', 'GOL01', 'A01', 'VGA HIS H240F2G', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 649),
('VGA0034K', 'M01', 'GOL01', 'A01', 'VGA HIS H240FC2G', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 650),
('VGA0032', 'M01', 'GOL01', 'A01', 'VGA SUPER GRAPHIC', '-', '-', '-', 0, 0, 0, 640000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 651),
('VID0002', 'M01', 'GOL01', 'A01', 'VIDEO BALLON UTK KABEL LAN KE CCTV', '-', '-', '-', 0, 0, 0, 65000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 652),
('VID0001', 'M01', 'GOL01', 'A01', 'VIDIO DISTRIBUTOR', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 653),
('TIN0065', 'M01', 'GOL01', 'A01', 'WATER BASED INK CANON CYAN 100ML', '-', '-', '-', 0, 0, 0, 33000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 654),
('TIN0031', 'M01', 'GOL01', 'A01', 'WATER BASED INK CANON CYAN 200ML', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 655),
('TIN0032', 'M01', 'GOL01', 'A01', 'WATER BASED INK CANON MAGENTA 200ML', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 656),
('TIN0033', 'M01', 'GOL01', 'A01', 'WATER BASED INK CANON YELLOW 200ML', '-', '-', '-', 0, 0, 0, 50000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 657),
('TIN0067', 'M01', 'GOL01', 'A01', 'WATER BASED INK CANON YELOW 100ML', '-', '-', '-', 0, 0, 0, 33000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 658),
('TIN0054', 'M01', 'GOL01', 'A01', 'WATER BASED INK HP CYAN 200ML', '-', '-', '-', 0, 0, 0, 55000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 659),
('TIN0055', 'M01', 'GOL01', 'A01', 'WATER BASED INK HP MAGENTA 200ML', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 660),
('TIN0053', 'M01', 'GOL01', 'A01', 'WATER BASED INK HP YELLOW 200ML', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 661),
('TIN0059', 'M01', 'GOL01', 'A01', 'WBI EPSON YELLOW 200ML', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 662),
('WIN0006', 'M01', 'GOL01', 'A01', 'WINDOWS 7 BASIC', '-', '-', '-', 0, 0, 0, 1250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 663),
('WIN0011', 'M01', 'GOL01', 'A01', 'WINDOWS 8 PRO 64BIT', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 664),
('WIR0014', 'M01', 'GOL01', 'A01', 'WIRELES TENDA USB W311MA', '-', '-', '-', 0, 0, 0, 160000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 665),
('WIR0026', 'M01', 'GOL01', 'A01', 'WIRELESS CARD TP-LINK PCI', '-', '-', '-', 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 666),
('WIR0025', 'M01', 'GOL01', 'A01', 'WIRELESS CARD TP-LINK PCI-E', '-', '-', '-', 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 667),
('WIR0024', 'M01', 'GOL01', 'A01', 'WIRELESS EXTENDER TP LINK 300MBPS WA860', '-', '-', '-', 0, 0, 0, 485000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 668),
('WIR0011', 'M01', 'GOL01', 'A01', 'WIRELESS FUNCTION ROUTER N300 EDIMAX', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 669),
('WIR0016', 'M01', 'GOL01', 'A01', 'WIRELESS HIGH POWER ROUTER W309R', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 670),
('WIR0015', 'M01', 'GOL01', 'A01', 'WIRELESS HOME ROUTER N301', '-', '-', '-', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 671),
('WIR0003', 'M01', 'GOL01', 'A01', 'WIRELESS LAN USB D-LINK', '-', '-', '-', 0, 0, 0, 270000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 672),
('WIR0001', 'M01', 'GOL01', 'A01', 'WIRELESS LAN USB TP-LINK', '-', '-', '-', 0, 0, 0, 160000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 673),
('WIR0009', 'M01', 'GOL01', 'A01', 'WIRELESS LAN USB TP-LINK WN722N', '-', '-', '-', 0, 0, 0, 145000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 674),
('WIR0012', 'M01', 'GOL01', 'A01', 'WIRELESS N ROUTER NETIS WF2419/2411', '-', '-', '-', 0, 0, 0, 265000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 675),
('WIR0023', 'M01', 'GOL01', 'A01', 'WIRELESS N300 RANGE EXTENDER TENDA/ A301', '-', '-', '-', 0, 0, 0, 300000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 676),
('WIR0021', 'M01', 'GOL01', 'A01', 'WIRELESS TENDA 0151', '-', '-', '-', 0, 0, 0, 250000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 677),
('WIR0020', 'M01', 'GOL01', 'A01', 'WIRELESS TENDA FH 1202', '-', '-', '-', 0, 0, 0, 850000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 678);
INSERT INTO `zstok` (`kodebrg`, `kodemerk`, `kodegol`, `kodegrup`, `namabrg`, `satuan1`, `satuan2`, `satuan3`, `isi1`, `isi2`, `hrgbeli`, `harga1`, `harga11`, `harga111`, `harga2`, `harga22`, `harga222`, `harga3`, `harga33`, `harga333`, `harga4`, `harga44`, `harga444`, `harga5`, `harga55`, `harga555`, `harga6`, `harga66`, `harga666`, `autoid`) VALUES
('WIR0019', 'M01', 'GOL01', 'A01', 'WIRELESS TENDA FH 303/H P AC1200', '-', '-', '-', 0, 0, 0, 750000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 679),
('WIR0017', 'M01', 'GOL01', 'A01', 'WIRELESS TENDA W309R', '-', '-', '-', 0, 0, 0, 500000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 680),
('WIR0007', 'M01', 'GOL01', 'A01', 'WIRELESS TP-LINK OUTDOOR WA5210G/WR941ND', '-', '-', '-', 0, 0, 0, 660000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 681);

-- --------------------------------------------------------

--
-- Table structure for table `zsupplier`
--

CREATE TABLE `zsupplier` (
  `kodesup` char(10) NOT NULL,
  `namasup` tinytext NOT NULL,
  `alamat` tinytext NOT NULL,
  `kota` tinytext NOT NULL,
  `ktp` char(20) NOT NULL,
  `npwp` char(30) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `zsupplier`
--

INSERT INTO `zsupplier` (`kodesup`, `namasup`, `alamat`, `kota`, `ktp`, `npwp`) VALUES
('S001', 'PT. INDOFOOD SUKSES MAKMUR', 'JL RAYA SERPONG', 'TANGERANG', 'TIDAK ADA', 'TIDAK ADA'),
('S002', 'PT. ABC', 'JL DAAN MOGOOT', 'JAKARTA', 'TIDAK ADA', 'TIDAK ADA');

-- --------------------------------------------------------

--
-- Table structure for table `ztipe`
--

CREATE TABLE `ztipe` (
  `kodetipe` char(20) NOT NULL,
  `namatipe` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `ztipe`
--

INSERT INTO `ztipe` (`kodetipe`, `namatipe`) VALUES
('TP01', 'PERUSAHAAN'),
('TP02', 'BIASA');

-- --------------------------------------------------------

--
-- Table structure for table `zusers`
--

CREATE TABLE `zusers` (
  `kodeuser` char(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `kunci` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `zusers`
--

INSERT INTO `zusers` (`kodeuser`, `username`, `kunci`) VALUES
('USER00', 'ADMINTEST', 'ADMINTEST'),
('USER01', 'USER1', 'USER1');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `zbeli`
--
ALTER TABLE `zbeli`
  ADD UNIQUE KEY `nonota` (`nonota`);

--
-- Indexes for table `zgolongan`
--
ALTER TABLE `zgolongan`
  ADD PRIMARY KEY (`kodegol`);

--
-- Indexes for table `zgrup`
--
ALTER TABLE `zgrup`
  ADD PRIMARY KEY (`kodegrup`);

--
-- Indexes for table `zgudang`
--
ALTER TABLE `zgudang`
  ADD PRIMARY KEY (`kodegd`);

--
-- Indexes for table `zjual`
--
ALTER TABLE `zjual`
  ADD UNIQUE KEY `nonota` (`nonota`);

--
-- Indexes for table `zkustomer`
--
ALTER TABLE `zkustomer`
  ADD PRIMARY KEY (`kodekust`);

--
-- Indexes for table `zmerek`
--
ALTER TABLE `zmerek`
  ADD PRIMARY KEY (`kodemerk`);

--
-- Indexes for table `zmutasi`
--
ALTER TABLE `zmutasi`
  ADD PRIMARY KEY (`nonota`);

--
-- Indexes for table `zsales`
--
ALTER TABLE `zsales`
  ADD PRIMARY KEY (`kodesls`);

--
-- Indexes for table `zstok`
--
ALTER TABLE `zstok`
  ADD PRIMARY KEY (`autoid`),
  ADD UNIQUE KEY `kodebrg` (`kodebrg`);

--
-- Indexes for table `zsupplier`
--
ALTER TABLE `zsupplier`
  ADD PRIMARY KEY (`kodesup`);

--
-- Indexes for table `ztipe`
--
ALTER TABLE `ztipe`
  ADD PRIMARY KEY (`kodetipe`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
