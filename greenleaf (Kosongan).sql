-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Jan 21, 2016 at 09:33 AM
-- Server version: 10.1.9-MariaDB
-- PHP Version: 5.6.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `greenleaf`
--

-- --------------------------------------------------------

--
-- Table structure for table `nota`
--

CREATE TABLE `nota` (
  `id_nota` int(255) NOT NULL,
  `tanggalcetak_nota` datetime NOT NULL,
  `nomorruangan_nota` varchar(255) NOT NULL,
  `jamkerja_nota` varchar(255) NOT NULL,
  `tamuhotel_nota` varchar(255) NOT NULL,
  `potonganhotel_nota` decimal(15,0) NOT NULL,
  `namapaket_nota` varchar(255) NOT NULL,
  `hargapaket_nota` decimal(15,0) NOT NULL,
  `extra_nota` varchar(255) NOT NULL,
  `nominalextra_nota` decimal(15,0) NOT NULL,
  `kodeterapis_nota` int(255) NOT NULL,
  `namaterapis_nota` varchar(255) NOT NULL,
  `diskon_nota` decimal(15,0) NOT NULL,
  `keterangan_nota` text NOT NULL,
  `feeterapis_nota` decimal(15,0) NOT NULL,
  `totalbayar_nota` decimal(15,0) NOT NULL,
  `grandtotal_nota` decimal(15,0) NOT NULL,
  `jenisbayar_nota` varchar(255) NOT NULL,
  `status_nota` varchar(255) NOT NULL,
  `id_paket` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `paket`
--

CREATE TABLE `paket` (
  `id_paket` int(255) NOT NULL,
  `jenis_paket` varchar(255) NOT NULL,
  `nama_paket` varchar(255) NOT NULL,
  `durasi_paket` varchar(255) NOT NULL,
  `harga_paket` decimal(15,0) NOT NULL,
  `komisi_normal_paket` double NOT NULL,
  `komisi_midnight_paket` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `pengguna`
--

CREATE TABLE `pengguna` (
  `id_pengguna` int(10) NOT NULL,
  `nama_pengguna` varchar(100) NOT NULL,
  `kata_kunci` varchar(50) NOT NULL,
  `jenis_pengguna` varchar(20) NOT NULL,
  `namaasli_pengguna` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `pengguna`
--

INSERT INTO `pengguna` (`id_pengguna`, `nama_pengguna`, `kata_kunci`, `jenis_pengguna`, `namaasli_pengguna`) VALUES
(1, 'superadmin', 'superadmin', 'Superadmin', 'superadmin'),
(2, 'sales', 'sales', 'Sales', 'sales');

-- --------------------------------------------------------

--
-- Table structure for table `terapis`
--

CREATE TABLE `terapis` (
  `id_terapis` int(255) NOT NULL,
  `kode_terapis` int(255) NOT NULL,
  `nama_terapis` varchar(100) NOT NULL,
  `lokasi_gambar` text NOT NULL,
  `status_terapis` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `variabel`
--

CREATE TABLE `variabel` (
  `id_variabel` int(255) NOT NULL,
  `extra_variabel` int(255) NOT NULL,
  `potonganhotel_variabel` int(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `variabel`
--

INSERT INTO `variabel` (`id_variabel`, `extra_variabel`, `potonganhotel_variabel`) VALUES
(1, 50, 100000);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `nota`
--
ALTER TABLE `nota`
  ADD PRIMARY KEY (`id_nota`);

--
-- Indexes for table `paket`
--
ALTER TABLE `paket`
  ADD PRIMARY KEY (`id_paket`);

--
-- Indexes for table `pengguna`
--
ALTER TABLE `pengguna`
  ADD PRIMARY KEY (`id_pengguna`);

--
-- Indexes for table `terapis`
--
ALTER TABLE `terapis`
  ADD PRIMARY KEY (`id_terapis`);

--
-- Indexes for table `variabel`
--
ALTER TABLE `variabel`
  ADD PRIMARY KEY (`id_variabel`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `nota`
--
ALTER TABLE `nota`
  MODIFY `id_nota` int(255) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `paket`
--
ALTER TABLE `paket`
  MODIFY `id_paket` int(255) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `pengguna`
--
ALTER TABLE `pengguna`
  MODIFY `id_pengguna` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `terapis`
--
ALTER TABLE `terapis`
  MODIFY `id_terapis` int(255) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `variabel`
--
ALTER TABLE `variabel`
  MODIFY `id_variabel` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
