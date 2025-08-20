-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 19, 2025 at 08:49 AM
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
-- Database: `kita`
--

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
('AR02', 'SUNGAI RAYA');

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
  `ket` varchar(1000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zbeli`
--

INSERT INTO `zbeli` (`nonota`, `tgl`, `kodesup`, `kodegd`, `nilai`, `lunas`, `tgltempo`, `ppn`, `hppn`, `disc1`, `hdisc1`, `disc2`, `hdisc2`, `disc3`, `hdisc3`, `ket`) VALUES
('B001/25', '2025-01-01', 'S001', '', 1000000, 0, NULL, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, NULL),
('TS3001/25', '2025-04-01', 'S002', '', 2044130000, 0, NULL, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, NULL),
('BS001/25', '2025-03-01', 'S001', '', 4200000, 0, NULL, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, NULL),
('AB001/25', '2025-02-01', 'S001', '', 300000, 0, NULL, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, NULL),
('TCD3001/25', '2025-05-01', 'S002', '', 2030000, 0, NULL, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, NULL),
('TQR3401/25', '2025-06-05', 'S002', '', 20450000, 0, NULL, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, NULL),
('N001', '2025-08-12', 'S002', 'G01', 74393.9, 0, '2025-08-12', 11.00, 6381.38, 10.00, 7142.20, 5.00, 3213.99, 5.00, 3053.29, NULL),
('N003', '2025-08-12', 'S001', 'G01', 16781.27, 0, '2025-08-12', 11.00, 1662.02, 10.00, 2072.60, 10.00, 1865.34, 10.00, 1678.81, NULL),
('N002', '2025-08-13', 'S002', 'G01', 141567.79, 0, '2025-08-13', 11.00, 14029.24, 10.00, 17495.00, 10.00, 15745.50, 10.00, 14170.95, NULL),
('N004', '2025-08-13', 'S001', 'G01', 240038.11, 0, '2025-08-13', 11.00, 23787.56, 5.00, 12611.20, 5.00, 11980.64, 5.00, 11381.61, NULL),
('NB001', '2025-08-14', 'S001', 'G01', 214097.14, 0, '2025-08-14', 11.00, 20225.84, 10.00, 25222.40, 10.00, 22700.16, 10.00, 20430.14, NULL),
('NB002', '2025-08-14', 'S002', 'G01', 164620, 0, '2025-08-14', 11.00, 16313.69, 10.00, 20343.80, 10.00, 18309.42, 10.00, 16478.48, NULL),
('N0012', '2025-08-18', 'S002', 'G01', 3794174.15, 0, '2025-08-18', 11.00, 366089.33, 1.00, 34299.58, 1.00, 33956.58, 1.00, 33617.02, 'BELI BARANG SUSU');

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
  `jumlah` double DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zbelim`
--

INSERT INTO `zbelim` (`nonota`, `kodebrg`, `kodegd`, `jlh1`, `jlh2`, `jlh3`, `harga`, `disca`, `hdisca`, `discb`, `hdiscb`, `discc`, `hdiscc`, `discrp`, `jumlah`) VALUES
('B001/25', 'D01', '', 8.627946919295937, 3.1215459760278463, 0, 5933.197259437293, 1, 0, 2, 0, 3, 0, 4, 75208.36512558162),
('B001/25', 'L01', '', 7.5161552731879056, 0, 0, 6390.347443521023, 1, 0, 2, 0, 3, 0, 4, 55715.67991282791),
('B001/25', 'LA12', '', 5.735735786147416, 3.9914962300099432, 7.463492164388299, 8907.707321923226, 1, 0, 2, 0, 3, 0, 4, 31044.0958943218),
('TS3001/25', 'D01', '', 5.399991918820888, 9.276096429675817, 0, 9878.461507614702, 1, 0, 2, 0, 3, 0, 4, 92705.19898273051),
('TS3001/25', 'L01', '', 5.313898904714733, 0, 0, 6060.769511386752, 1, 0, 2, 0, 3, 0, 4, 45468.74619554728),
('TS3001/25', 'LA12', '', 8.007558095268905, 0.9786076028831303, 2.735431045293808, 6659.439520444721, 1, 0, 2, 0, 3, 0, 4, 51662.32665069401),
('BS001/25', 'D01', '', 0.7693073176778853, 5.668081538751721, 0, 7353.739466052502, 1, 0, 2, 0, 3, 0, 4, 18269.350146874785),
('BS001/25', 'L01', '', 5.563666906673461, 0, 0, 5599.77600350976, 1, 0, 2, 0, 3, 0, 4, 91727.30331774801),
('BS001/25', 'LA12', '', 4.688533418811858, 0.43678106972947717, 6.007050713524222, 7041.913464199752, 1, 0, 2, 0, 3, 0, 4, 82247.4509011954),
('SB3001/25', 'D01', '', 5.584632263053209, 2.685053087770939, 0, 1365.6491576693952, 1, 0, 2, 0, 3, 0, 4, 56393.36374588311),
('SB3001/25', 'L01', '', 7.882318238262087, 0, 0, 2832.6246980577707, 1, 0, 2, 0, 3, 0, 4, 8988.60755842179),
('SB3001/25', 'LA12', '', 8.151401071809232, 6.983295206446201, 4.4205874018371105, 5550.379378255457, 1, 0, 2, 0, 3, 0, 4, 46419.66880299151),
('ST3401/25', 'D01', '', 2.3658866272307932, 9.124482600018382, 0, 1774.1792718879879, 1, 0, 2, 0, 3, 0, 4, 61931.79967813194),
('ST3401/25', 'L01', '', 0.23330762283876538, 0, 0, 4603.0136197805405, 1, 0, 2, 0, 3, 0, 4, 57047.537877224386),
('ST3401/25', 'LA12', '', 2.551317666657269, 1.6734550171531737, 4.403441222384572, 1692.8819823078811, 1, 0, 2, 0, 3, 0, 4, 448.79294000566006),
('AB001/25', 'D01', '', 5.984712999779731, 6.275182235985994, 0, 6398.035844322294, 1, 0, 2, 0, 3, 0, 4, 29890.050971880555),
('AB001/25', 'L01', '', 5.574024415109307, 0, 0, 2172.861313447356, 1, 0, 2, 0, 3, 0, 4, 10936.78146135062),
('AB001/25', 'LA12', '', 3.1074733240529895, 1.3334776763804257, 5.730065815150738, 7341.700920369476, 1, 0, 2, 0, 3, 0, 4, 40035.25418229401),
('TCD3001/25', 'D01', '', 9.90165930474177, 5.48505381681025, 0, 3692.2545614652336, 1, 0, 2, 0, 3, 0, 4, 2107.6933946460485),
('TCD3001/25', 'L01', '', 6.7160220933146775, 0, 0, 4639.665242284536, 1, 0, 2, 0, 3, 0, 4, 35652.17552240938),
('TCD3001/25', 'LA12', '', 2.8554110089316964, 1.2625188962556422, 0.6326861213892698, 6969.556815456599, 1, 0, 2, 0, 3, 0, 4, 64610.63879542053),
('TQR3401/25', 'D01', '', 3.4287520428188145, 0.30891597270965576, 0, 1216.8286903761327, 1, 0, 2, 0, 3, 0, 4, 39273.86631257832),
('TQR3401/25', 'L01', '', 2.366888376418501, 0, 0, 8965.18831141293, 1, 0, 2, 0, 3, 0, 4, 82850.70534329861),
('TQR3401/25', 'LA12', '', 9.213798991404474, 1.772589918691665, 4.054594282060862, 8989.332949277014, 1, 0, 2, 0, 3, 0, 4, 97595.25679983199),
('TEST12', 'D01', 'G01', 1, 2, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 24000),
('N001', 'D01', 'G01', 1, 2, 0, 24000, 10, 25920, 10, 23328, 10, 20995.2, 10, 20985),
('N001', 'LA12', 'G02', 1, 2, 3, 60000, 10, 62280, 10, 56052, 10, 50446.8, 10, 50437),
('N003', 'D01', 'G01', 1, 2, 0, 30000, 20, 28800, 10, 25920, 20, 20736, 10, 20726),
('N002', 'D01', 'G01', 1, 2, 0, 200000, 10, 216000, 10, 194400, 10, 174960, 10, 174950),
('N004', 'LA12', 'G01', 1, 2, 3, 300000, 10, 311400, 10, 280260, 10, 252234, 10, 252224),
('NB001', 'LA12', 'G01', 1, 2, 3, 300000, 10, 311400, 10, 280260, 10, 252234, 10, 252224),
('NB002', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'D01', 'G01', 1, 2, 0, 200000, 10, 216000, 10, 194400, 10, 174960, 10, 174950),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438),
('N0012', 'LA12', 'G01', 1, 2, 3, 350000, 10, 363300, 20, 290640, 30, 203448, 10, 203438);

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
('GOL01', 'GOLONGAN 1');

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
  `ket` varchar(1000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zjual`
--

INSERT INTO `zjual` (`nonota`, `tgl`, `kodekust`, `kodesls`, `kodegd`, `nilai`, `lunas`, `tgltempo`, `ppn`, `hppn`, `disc1`, `hdisc1`, `disc2`, `hdisc2`, `disc3`, `hdisc3`, `ket`) VALUES
('CA001', '2025-08-13', 'C05', 'S02', 'G02', 65979, 0, '2025-08-13', 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, NULL),
('NJ001', '2025-08-14', 'C01', 'S02', 'G01', 64322.26, 0, '2025-08-14', 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, NULL),
('NJ003', '2025-08-14', 'C02', 'S01', 'G01', 214097.14, 0, '2025-08-14', 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, NULL),
('NJ002', '2025-08-14', 'C02', 'S02', 'G01', 112044.52, 0, '2025-08-14', 0.00, 0.00, 10.00, 12610.70, 10.00, 11349.63, 10.00, 10214.67, NULL),
('NJ004', '2025-08-14', 'C01', 'S02', 'G01', 42265.3, 0, '2025-08-14', 11.00, 3692.96, 1.00, 346.00, 1.00, 342.54, 1.00, 339.11, NULL),
('NJ012', '2025-08-18', 'C02', 'S02', 'G01', 198426.73, 0, '2025-08-18', 11.00, 18672.92, 1.00, 1749.50, 1.00, 1732.01, 1.00, 1714.68, 'JUAL BARANG SUSU');

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
  `jumlah` double DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zjualm`
--

INSERT INTO `zjualm` (`nonota`, `kodebrg`, `kodegd`, `jlh1`, `jlh2`, `jlh3`, `harga`, `disca`, `hdisca`, `discb`, `hdiscb`, `discc`, `hdiscc`, `discrp`, `jumlah`) VALUES
('CA001', 'D01', 'G02', 1, 2, 0, 20000, 10, 21600, 20, 17280, 10, 15552, 10, 15542),
('CA001', 'LA12', 'G01', 1, 2, 3, 60000, 10, 62280, 10, 56052, 10, 50446.8, 10, 50437),
('NJ001', 'LA12', 'G01', 1, 2, 3, 60000, 10, 62280, 10, 56052, 10, 50446.8, 10, 50437),
('NJ003', 'LA12', 'G01', 1, 2, 3, 300000, 10, 311400, 10, 280260, 10, 252234, 10, 252224),
('NJ002', 'LA12', 'G01', 1, 2, 3, 150000, 10, 155700, 10, 140130, 10, 126117, 10, 126107),
('NJ002', 'LA12', 'G01', 1, 2, 3, 150000, 10, 155700, 10, 140130, 10, 126117, 10, 126107),
('NJ002', 'LA12', 'G01', 1, 2, 3, 150000, 10, 155700, 10, 140130, 10, 126117, 10, 126107),
('NJ004', 'LA12', 'G01', 1, 2, 3, 30000, 0, 34600, 0, 34600, 0, 34600, 0, 34600),
('NJ012', 'D01', 'G01', 1, 2, 0, 200000, 10, 216000, 10, 194400, 10, 174960, 10, 174950);

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
('C01', NULL, NULL, 'PT MEDIA INDAH', 'JL. MENTARI', 'PONTIANAK', '2', '3201021501010001', '12.345.678.9-012.345'),
('C02', NULL, NULL, 'PT ASIA JAYA ABADI', 'JL. SITAPANG', 'SINGKAWANG', '5', '3201021501010002', '12.345.678.9-013.345');

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
('D01', 'M01', 'GOL01', 'A02', 'SUSU DANCOW BALITA MADU', 'DUS', 'KTK', '-', 10, 0, 20000, 100000, 10000, 1000, 200000, 20000, 2000, 300000, 30000, 3000, 400000, 40000, 4000, 500000, 50000, 5000, 600000, 60000, 6000, 1),
('L01', NULL, NULL, 'A03', 'KERTAS KADO', 'LBR', '-', '-', 0, 0, 10000, 200000, 20000, 2000, 400000, 40000, 4000, 600000, 60000, 6000, 800000, 80000, 8000, 1000000, 100000, 10000, 1200000, 120000, 12000, 2),
('LA12', NULL, NULL, 'A01', 'ROKOK LA 12 BATANG', 'DUS', 'SLOP', 'BKS', 15, 10, 30000, 300000, 30000, 3000, 600000, 60000, 6000, 900000, 90000, 9000, 1200000, 120000, 12000, 1500000, 150000, 15000, 1800000, 180000, 18000, 3),
('BUK01', 'M01', 'GOL01', 'A01', 'BOX SIDU', 'DUS', 'KTK', '-', 2, 1, 35000, 1000, 10000, 0, 2000, 20000, 0, 3000, 30000, 0, 4000, 40000, 0, 5000, 50000, 0, 6000, 60000, 0, 4);

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
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `zusers`
--

INSERT INTO `zusers` (`kodeuser`, `username`, `password`) VALUES
('USR00000000', 'ADMINTEST', 'admintest');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `zarea`
--
ALTER TABLE `zarea`
  ADD PRIMARY KEY (`kodear`);

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

--
-- Indexes for table `zusers`
--
ALTER TABLE `zusers`
  ADD PRIMARY KEY (`kodeuser`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `zstok`
--
ALTER TABLE `zstok`
  MODIFY `autoid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
