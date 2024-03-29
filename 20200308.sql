USE [master]
GO
/****** Object:  Database [EticaretDB]    Script Date: 8.03.2020 14:03:18 ******/
CREATE DATABASE [EticaretDB]
GO
USE [EticaretDB]
GO
/****** Object:  Table [dbo].[Bannerlar]    Script Date: 8.03.2020 14:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bannerlar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Resim] [nvarchar](max) NULL,
	[SiraNo] [int] NULL,
	[AktifMi] [bit] NULL,
	[SilindiMi] [bit] NULL,
	[KayitTarihi] [datetime] NULL,
	[SilinmeTarihi] [datetime] NULL,
 CONSTRAINT [PK_Bannerlar] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kategoriler]    Script Date: 8.03.2020 14:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kategoriler](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Adi] [nvarchar](max) NULL,
	[KullaniciID] [int] NULL,
	[AktifMi] [bit] NULL,
	[SilindiMi] [bit] NULL,
	[KayitTarihi] [datetime] NULL,
	[SilinmeTarihi] [datetime] NULL,
 CONSTRAINT [PK_Kategoriler] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kullanicilar]    Script Date: 8.03.2020 14:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kullanicilar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Ad] [nvarchar](50) NULL,
	[Soyad] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Parola] [nvarchar](max) NULL,
	[AktifMi] [bit] NULL,
	[SilindiMi] [bit] NULL,
	[KayitTarihi] [datetime] NULL,
	[SilinmeTarihi] [datetime] NULL,
 CONSTRAINT [PK_Kullanicilar] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SayfaKategorileri]    Script Date: 8.03.2020 14:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SayfaKategorileri](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Adi] [nvarchar](max) NULL,
	[Aciklama] [ntext] NULL,
	[AktifMi] [bit] NULL,
	[SilindiMi] [bit] NULL,
	[KayitTarihi] [datetime] NULL,
	[SilinmeTarihi] [datetime] NULL,
 CONSTRAINT [PK_SayfaKategorileri] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sayfalar]    Script Date: 8.03.2020 14:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sayfalar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Ad] [nvarchar](max) NULL,
	[Aciklama] [ntext] NULL,
	[KategoriID] [int] NULL,
	[AktifMi] [bit] NULL,
	[SilindiMi] [bit] NULL,
	[KayitTarihi] [datetime] NULL,
	[SilinmeTarihi] [datetime] NULL,
 CONSTRAINT [PK_Sayfalar] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sepet]    Script Date: 8.03.2020 14:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sepet](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UyeID] [int] NULL,
	[AktifMi] [bit] NULL,
	[SilindiMi] [bit] NULL,
	[KayitTarihi] [datetime] NULL,
	[SilinmeTarihi] [datetime] NULL,
	[SiparisNumarasi] [nvarchar](50) NULL,
	[SiparisDurumu] [int] NULL,
 CONSTRAINT [PK_Sepet] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SepetDetay]    Script Date: 8.03.2020 14:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SepetDetay](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[SepetID] [int] NULL,
	[UrunID] [int] NULL,
	[Fiyat] [money] NULL,
	[indirim] [float] NULL,
	[Adet] [int] NULL,
	[Renk] [nvarchar](50) NULL,
	[Numara] [nvarchar](50) NULL,
	[AktifMi] [bit] NULL,
	[SilindiMi] [bit] NULL,
	[KayitTarihi] [datetime] NULL,
	[SilinmeTarihi] [datetime] NULL,
 CONSTRAINT [PK_SepetDetay] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Urunler]    Script Date: 8.03.2020 14:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Urunler](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Adi] [nvarchar](500) NULL,
	[KategoriID] [int] NULL,
	[Fiyat] [money] NULL,
	[indirim] [float] NULL,
	[Aciklama] [ntext] NULL,
	[TedarikSureci] [int] NULL,
	[AktifMi] [bit] NULL,
	[SilindiMi] [bit] NULL,
	[KayitTarihi] [datetime] NULL,
	[SilinmeTarihi] [datetime] NULL,
	[KullaniciID] [int] NULL,
	[RenkBilgisi] [nvarchar](max) NULL,
	[NumaraBilgisi] [nvarchar](max) NULL,
 CONSTRAINT [PK_Urunler] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrunNumaralari]    Script Date: 8.03.2020 14:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrunNumaralari](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Numara] [nvarchar](10) NULL,
	[AktifMi] [bit] NULL,
	[SilindiMi] [bit] NULL,
	[KayitTarihi] [datetime] NULL,
	[SilinmeTarihi] [datetime] NULL,
 CONSTRAINT [PK_UrunNumaralari] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrunRenkleri]    Script Date: 8.03.2020 14:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrunRenkleri](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Renk] [nvarchar](10) NULL,
	[AktifMi] [bit] NULL,
	[SilindiMi] [bit] NULL,
	[KayitTarihi] [datetime] NULL,
	[SilinmeTarihi] [datetime] NULL,
 CONSTRAINT [PK_UrunRenkleri] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrunResimleri]    Script Date: 8.03.2020 14:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrunResimleri](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UrunID] [int] NULL,
	[Resim] [nvarchar](max) NULL,
	[SiraNo] [int] NULL,
	[AktifMi] [bit] NULL,
	[SilindiMi] [bit] NULL,
	[KayitTarihi] [datetime] NULL,
	[SilinmeTarihi] [datetime] NULL,
 CONSTRAINT [PK_UrunResimleri] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Uyeler]    Script Date: 8.03.2020 14:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Uyeler](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Ad] [nvarchar](50) NULL,
	[Soyad] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Telefon] [nvarchar](50) NULL,
	[Adres] [nvarchar](max) NULL,
	[Parola] [nvarchar](max) NULL,
	[Cinsiyet] [int] NULL,
	[TcNo] [bigint] NULL,
	[DogumTarihi] [date] NULL,
	[AktifMi] [bit] NULL,
	[SilindiMi] [bit] NULL,
	[KayitTarihi] [datetime] NULL,
	[SilinmeTarihi] [datetime] NULL,
 CONSTRAINT [PK_Uyeler] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Kategoriler]  WITH CHECK ADD  CONSTRAINT [FK_Kategoriler_Kullanicilar] FOREIGN KEY([KullaniciID])
REFERENCES [dbo].[Kullanicilar] ([id])
GO
ALTER TABLE [dbo].[Kategoriler] CHECK CONSTRAINT [FK_Kategoriler_Kullanicilar]
GO
ALTER TABLE [dbo].[Sayfalar]  WITH CHECK ADD  CONSTRAINT [FK_Sayfalar_SayfaKategorileri] FOREIGN KEY([KategoriID])
REFERENCES [dbo].[SayfaKategorileri] ([id])
GO
ALTER TABLE [dbo].[Sayfalar] CHECK CONSTRAINT [FK_Sayfalar_SayfaKategorileri]
GO
ALTER TABLE [dbo].[Sepet]  WITH CHECK ADD  CONSTRAINT [FK_Sepet_Uyeler] FOREIGN KEY([UyeID])
REFERENCES [dbo].[Uyeler] ([id])
GO
ALTER TABLE [dbo].[Sepet] CHECK CONSTRAINT [FK_Sepet_Uyeler]
GO
ALTER TABLE [dbo].[SepetDetay]  WITH CHECK ADD  CONSTRAINT [FK_SepetDetay_Sepet] FOREIGN KEY([SepetID])
REFERENCES [dbo].[Sepet] ([id])
GO
ALTER TABLE [dbo].[SepetDetay] CHECK CONSTRAINT [FK_SepetDetay_Sepet]
GO
ALTER TABLE [dbo].[SepetDetay]  WITH CHECK ADD  CONSTRAINT [FK_SepetDetay_Urunler] FOREIGN KEY([UrunID])
REFERENCES [dbo].[Urunler] ([id])
GO
ALTER TABLE [dbo].[SepetDetay] CHECK CONSTRAINT [FK_SepetDetay_Urunler]
GO
ALTER TABLE [dbo].[Urunler]  WITH CHECK ADD  CONSTRAINT [FK_Urunler_Kategoriler] FOREIGN KEY([KategoriID])
REFERENCES [dbo].[Kategoriler] ([id])
GO
ALTER TABLE [dbo].[Urunler] CHECK CONSTRAINT [FK_Urunler_Kategoriler]
GO
ALTER TABLE [dbo].[Urunler]  WITH CHECK ADD  CONSTRAINT [FK_Urunler_Kullanicilar] FOREIGN KEY([KullaniciID])
REFERENCES [dbo].[Kullanicilar] ([id])
GO
ALTER TABLE [dbo].[Urunler] CHECK CONSTRAINT [FK_Urunler_Kullanicilar]
GO
ALTER TABLE [dbo].[UrunResimleri]  WITH CHECK ADD  CONSTRAINT [FK_UrunResimleri_Urunler] FOREIGN KEY([UrunID])
REFERENCES [dbo].[Urunler] ([id])
GO
ALTER TABLE [dbo].[UrunResimleri] CHECK CONSTRAINT [FK_UrunResimleri_Urunler]
GO
USE [master]
GO
ALTER DATABASE [EticaretDB] SET  READ_WRITE 
GO
