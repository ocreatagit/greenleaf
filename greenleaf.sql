-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Dec 26, 2015 at 02:52 AM
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
  `tanggalcetak_nota` varchar(255) NOT NULL,
  `nomorruangan_nota` int(255) NOT NULL,
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
  `totalbayar_nota` decimal(15,0) NOT NULL,
  `feeterapis_nota` decimal(15,0) NOT NULL,
  `jenisbayar_nota` varchar(255) NOT NULL,
  `status_nota` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `nota`
--

INSERT INTO `nota` (`id_nota`, `tanggalcetak_nota`, `nomorruangan_nota`, `jamkerja_nota`, `tamuhotel_nota`, `potonganhotel_nota`, `namapaket_nota`, `hargapaket_nota`, `extra_nota`, `nominalextra_nota`, `kodeterapis_nota`, `namaterapis_nota`, `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`) VALUES
(2, '26/12/2015 0:12:8', 2, 'Normal', 'Ya', '100000', 'VIP - Special Massage', '400000', 'Tidak', '0', 0, 'Ali', '0', '', '300000', '0', 'Cash', '-'),
(3, '26/12/2015 0:16:20', 1, 'Normal', 'Ya', '100000', 'VIP - Special Massage', '400000', 'Ya', '200000', 0, 'William', '50000', 'Gratis', '500000', '76000', 'Credit', '-'),
(4, '26/12/2015 0:17:27', 0, '', '', '0', '', '0', '', '0', 0, '', '0', '', '0', '0', '', '-'),
(5, '26/12/2015 0:17:37', 0, '', '', '0', '', '0', '', '0', 0, '', '0', '', '0', '0', '', '-'),
(6, '26/12/2015 0:17:41', 0, '', '', '0', '', '0', '', '0', 0, '', '0', '', '0', '0', '', '-'),
(7, '26/12/2015 0:21:24', 0, '', '', '0', '', '0', '', '0', 0, '', '0', '', '0', '0', '', '-'),
(8, '26/12/2015 0:32:11', 123, 'Normal', 'Ya', '100000', 'VIP - Special Massage', '400000', 'Ya', '200000', 23, 'William', '0', '', '500000', '0', 'Cash', '-'),
(9, '26/12/2015 0:32:15', 123, 'Normal', 'Ya', '100000', 'VIP - Special Massage', '400000', 'Ya', '200000', 23, 'William', '123', 'asdf', '500000', '123', 'Cash', '-'),
(10, '26/12/2015 0:32:17', 123, 'Normal', 'Ya', '100000', 'VIP - Special Massage', '400000', 'Ya', '200000', 23, 'William', '123', 'asdf', '500000', '0', 'Cash', '-'),
(11, '26/12/2015 0:32:21', 123, 'Normal', 'Ya', '100000', 'VIP - Special Massage', '400000', 'Ya', '200000', 23, 'William', '0', '', '500000', '123', 'Cash', '-'),
(12, '26/12/2015 0:33:24', 123, 'Normal', 'Ya', '150000', 'VIP - Special Massage', '400000', 'Ya', '180000', 123, 'lucu', '123', 'asdf', '430000', '123', 'Cash', '-'),
(13, '26/12/2015 0:33:35', 234, 'Normal', 'Ya', '150000', 'VIP - Special Massage', '400000', 'Ya', '180000', 23, 'William', '436', 'asdffgdsfg', '430000', '1235', 'Cash', '-'),
(14, '26/12/2015 0:33:39', 234, 'Normal', 'Ya', '150000', 'VIP - Special Massage', '400000', 'Ya', '180000', 23, 'William', '436', 'asdffgdsfg', '430000', '1235', 'Credit', '-'),
(15, '26/12/2015 0:34:10', 234, 'Midnight', 'Tidak', '0', 'Deluxe - Special Massage', '600000', 'Ya', '270000', 123, 'lucu', '436', 'asdffgdsfg', '870000', '1235', 'Cash', '-'),
(16, '26/12/2015 0:34:33', 213, 'Normal', 'Ya', '150000', 'VIP - Special Massage', '400000', 'Ya', '180000', 123, 'lucu', '213', 'asdf', '430000', '123', 'Cash', '-'),
(17, '26/12/2015 0:39:8', 123, 'Normal', 'Ya', '100000', 'VIP - Special Massage', '400000', 'Ya', '200000', 23, 'William', '0', '', '500000', '0', 'Cash', '-'),
(18, '26/12/2015 0:40:59', 123, 'Normal', 'Ya', '100000', 'VIP - Special Massage', '400000', 'Ya', '200000', 23, 'William', '0', '', '500000', '0', 'Cash', '-'),
(19, '26/12/2015 0:41:53', 123, 'Normal', 'Ya', '100000', 'VIP - Special Massage', '400000', 'Ya', '200000', 123, 'lucu', '0', '', '500000', '0', 'Cash', '-'),
(20, '26/12/2015 0:42:7', 123, 'Normal', 'Ya', '100000', 'VIP - Special Massage', '400000', 'Ya', '200000', 123, 'lucu', '123', 'asdf', '500000', '0', 'Cash', '-'),
(21, '26/12/2015 0:42:10', 123, 'Normal', 'Ya', '100000', 'VIP - Special Massage', '400000', 'Ya', '200000', 123, 'lucu', '123', 'asdf', '500000', '123', 'Cash', '-'),
(22, '26/12/2015 0:42:12', 123, 'Normal', 'Ya', '100000', 'VIP - Special Massage', '400000', 'Ya', '200000', 123, 'lucu', '0', '', '500000', '123', 'Cash', '-'),
(23, '26/12/2015 0:43:29', 123, 'Normal', 'Tidak', '0', 'Deluxe - Traditional Massage', '500000', 'Ya', '250000', 123, 'lucu', '21', 'asfd', '750000', '213', 'Cash', '-'),
(24, '26/12/2015 0:43:57', 123, 'Normal', 'Tidak', '0', 'Deluxe - Traditional Massage', '500000', 'Tidak', '0', 123, 'lucu', '21', 'asfd', '500000', '213', 'Cash', '-');

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
  `komisi_normal_paket` decimal(15,0) NOT NULL,
  `komisi_midnight_paket` decimal(15,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `paket`
--

INSERT INTO `paket` (`id_paket`, `jenis_paket`, `nama_paket`, `durasi_paket`, `harga_paket`, `komisi_normal_paket`, `komisi_midnight_paket`) VALUES
(1, 'Deluxe', 'Traditional Massage', '1 Jam 30 Menit', '500000', '200000', '250000'),
(2, 'Deluxe', 'Special Massage', '1 Jam 30 Menit', '600000', '375000', '400000'),
(3, 'VIP', 'Traditional Massage', '1 Jam 30 Menit', '350000', '175000', '200000'),
(4, 'VIP', 'Special Massage', '1 Jam 30 Menit', '400000', '200000', '250000'),
(5, 'VIP', 'Full Body Massage', '2 Jam 30 Menit', '750000', '350000', '500000');

-- --------------------------------------------------------

--
-- Table structure for table `pengguna`
--

CREATE TABLE `pengguna` (
  `id_pengguna` int(10) NOT NULL,
  `nama_pengguna` varchar(100) NOT NULL,
  `kata_kunci` varchar(50) NOT NULL,
  `jenis_pengguna` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `pengguna`
--

INSERT INTO `pengguna` (`id_pengguna`, `nama_pengguna`, `kata_kunci`, `jenis_pengguna`) VALUES
(1, 'superadmin', 'superadmin', 'Superadmin'),
(2, 'sales', 'sales', 'Sales');

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

--
-- Dumping data for table `terapis`
--

INSERT INTO `terapis` (`id_terapis`, `kode_terapis`, `nama_terapis`, `lokasi_gambar`, `status_terapis`) VALUES
(1, 23, 'William', 'C:\\Users\\Public\\Pictures\\Sample Pictures\\Hydrangeas.jpg', 'Aktif'),
(2, 321, 'haha', 'C:\\Users\\Public\\Pictures\\Sample Pictures\\Koala.jpg', 'Tidak Aktif'),
(3, 123, 'lucu', 'C:\\Users\\Public\\Pictures\\Sample Pictures\\Desert.jpg', 'Aktif'),
(4, 657, 'mungkin', 'C:\\Users\\Public\\Pictures\\Sample Pictures\\Chrysanthemum.jpg', 'Tidak Aktif'),
(5, 756, 'sfd', 'C:\\Users\\Public\\Pictures\\Sample Pictures\\Penguins.jpg', 'Aktif');

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
  ADD PRIMARY KEY (`id_terapis`),
  ADD UNIQUE KEY `kode_terapis` (`kode_terapis`);

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
  MODIFY `id_nota` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;
--
-- AUTO_INCREMENT for table `paket`
--
ALTER TABLE `paket`
  MODIFY `id_paket` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT for table `pengguna`
--
ALTER TABLE `pengguna`
  MODIFY `id_pengguna` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `terapis`
--
ALTER TABLE `terapis`
  MODIFY `id_terapis` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT for table `variabel`
--
ALTER TABLE `variabel`
  MODIFY `id_variabel` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
