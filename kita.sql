-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 06, 2025 at 12:14 PM
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
  `hdisc3` double(10,2) DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zbeli`
--

INSERT INTO `zbeli` (`nonota`, `tgl`, `kodesup`, `kodegd`, `nilai`, `lunas`, `tgltempo`, `ppn`, `hppn`, `disc1`, `hdisc1`, `disc2`, `hdisc2`, `disc3`, `hdisc3`) VALUES
('B001/25', '2025-01-01', 'S001', '', 1000000, 0, NULL, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00),
('TS3001/25', '2025-04-01', 'S002', '', 2044130000, 0, NULL, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00),
('BS001/25', '2025-03-01', 'S001', '', 4200000, 0, NULL, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00),
('AB001/25', '2025-02-01', 'S001', '', 300000, 0, NULL, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00),
('TCD3001/25', '2025-05-01', 'S002', '', 2030000, 0, NULL, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00),
('TQR3401/25', '2025-06-05', 'S002', '', 20450000, 0, NULL, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00),
('X1', '2025-08-01', 'S002', '', 1, 0, '0000-00-00', 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00),
('X3', '2025-08-01', 'S002', '', 1, 0, '2025-08-02', 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00),
('AC001', '2025-08-05', 'S002', '', 432900, 0, '2025-08-05', 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00),
('ADAFE', '2025-08-05', 'S001', '', 49654, 0, '2025-08-05', 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00),
('NOTA5825', '2025-08-05', 'S002', 'G01', 1617640, 0, '2025-08-05', 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00);

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
('X1', 'L01', '', 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1),
('X3', 'L01', '', 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1),
('X2', 'L01', '', 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1),
('X4', 'L01', '', 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1),
('TEST12', 'D01', 'G01', 1, 2, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 24000),
('AC001', 'L01', 'G02', 13, 0, 0, 30000, 0, 0, 0, 0, 0, 0, 0, 390000),
('AC001', 'L01', 'G02', 13, 0, 0, 30000, 0, 0, 0, 0, 0, 0, 0, 390000),
('ADAFE', 'LA12', 'G01', 2, 1, 1, 10000, 0, 0, 0, 0, 0, 0, 0, 20733),
('ADAFE', 'D01', 'G02', 1, 2, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 24000),
('NOTA5825', 'D01', 'G01', 12, 1, 0, 20000, 0, 0, 0, 0, 0, 0, 0, 242000),
('NOTA5825', 'LA12', 'G03', 12, 2, 3, 100000, 0, 0, 0, 0, 0, 0, 0, 1215333);

-- --------------------------------------------------------

--
-- Table structure for table `zconfig`
--

CREATE TABLE `zconfig` (
  `jmlharga` int(11) DEFAULT 12,
  `qppn` double(10,2) DEFAULT 11.00
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `zconfig`
--

INSERT INTO `zconfig` (`jmlharga`, `qppn`) VALUES
(6, 11.00);

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
  `tgltempo` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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

-- --------------------------------------------------------

--
-- Table structure for table `zkustomer`
--

CREATE TABLE `zkustomer` (
  `kodekust` char(10) NOT NULL,
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

INSERT INTO `zkustomer` (`kodekust`, `namakust`, `alamat`, `kota`, `kodehrg`, `ktp`, `npwp`) VALUES
('C05', 'AWEN', 'SDFSD', 'SDFS', '2', 'SF', 'S'),
('test', 'test', 'test', 'test', 'te', 'tset', 'tes'),
('c01', 'customer 01', 'sdfkj;lfjak', 'asfddf', '6', 'a', 's'),
('C02', 'PEDRO', 'ADA', 'ADA', 'AS', 'SAAA', 'SAD');

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
('S01', 'SALES1', 'TTS', 'TSES', 'SETS', 'TST');

-- --------------------------------------------------------

--
-- Table structure for table `zstok`
--

CREATE TABLE `zstok` (
  `kodebrg` char(20) NOT NULL,
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

INSERT INTO `zstok` (`kodebrg`, `kodegrup`, `namabrg`, `satuan1`, `satuan2`, `satuan3`, `isi1`, `isi2`, `hrgbeli`, `harga1`, `harga11`, `harga111`, `harga2`, `harga22`, `harga222`, `harga3`, `harga33`, `harga333`, `harga4`, `harga44`, `harga444`, `harga5`, `harga55`, `harga555`, `harga6`, `harga66`, `harga666`, `autoid`) VALUES
('D01', 'A02', 'SUSU DANCOW BALITA MADU', 'DUS', 'KTK', '-', 10, 0, 20000, 100000, 10000, 1000, 200000, 20000, 2000, 300000, 30000, 3000, 400000, 40000, 4000, 500000, 50000, 5000, 600000, 60000, 6000, 1),
('L01', 'A03', 'KERTAS KADO', 'LBR', '-', '-', 0, 0, 1, 200000, 20000, 2000, 400000, 40000, 4000, 600000, 60000, 6000, 800000, 80000, 8000, 1000000, 100000, 10000, 1200000, 120000, 12000, 2),
('LA12', 'A01', 'ROKOK LA 12 BATANG', 'DUS', 'SLOP', 'BKS', 15, 10, 0, 300000, 30000, 3000, 600000, 60000, 6000, 900000, 90000, 9000, 1200000, 120000, 12000, 1500000, 150000, 15000, 1800000, 180000, 18000, 3);

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
-- Indexes for table `zbeli`
--
ALTER TABLE `zbeli`
  ADD UNIQUE KEY `nonota` (`nonota`);

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
  MODIFY `autoid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
