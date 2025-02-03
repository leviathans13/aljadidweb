'use client';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { 
  faPrint, 
  faBoxes, 
  faPaintBrush, 
  faClock,
  faComments,
  faShieldAlt,
  faHeadset,
  faCheck,
  faFileAlt,
  faBook,
  faBox,
  faTshirt
} from '@fortawesome/free-solid-svg-icons';

export default function Layanan() {
  const mainServices = [
    {
      icon: faPrint,
      title: "Digital Printing",
      description: "Layanan cetak digital berkualitas tinggi untuk kebutuhan cetak dengan jumlah terbatas dan waktu cepat.",
      features: [
        "Hasil cetak tajam dan detail",
        "Pilihan material beragam",
        "Waktu pengerjaan cepat",
        "Minimum order fleksibel"
      ],
      price: "Mulai dari Rp 2.500/lembar",
      consultation: true
    },
    {
      icon: faBoxes,
      title: "Offset Printing",
      description: "Solusi cetak untuk kebutuhan dalam jumlah besar dengan harga yang lebih ekonomis.",
      features: [
        "Kualitas cetak konsisten",
        "Harga ekonomis untuk jumlah besar",
        "Berbagai pilihan finishing",
        "Hasil cetak tahan lama"
      ],
      price: "Mulai dari Rp 1.000/lembar",
      consultation: true
    },
    {
      icon: faPaintBrush,
      title: "Desain Grafis",
      description: "Jasa desain profesional untuk memaksimalkan tampilan material cetak Anda.",
      features: [
        "Tim desainer berpengalaman",
        "Revisi hingga puas",
        "Konsep sesuai brand",
        "File siap cetak"
      ],
      price: "Mulai dari Rp 500.000/desain",
      consultation: true
    }
  ];

  const productCategories = [
    {
      icon: faFileAlt,
      title: "Marketing Kit",
      products: [
        {
          name: "Brosur",
          specs: "A4, Art Paper 150gsm, Full Color",
          price: "Rp 1.500/pcs (min. 100)",
          time: "2-3 hari kerja"
        },
        {
          name: "Flyer",
          specs: "A5, Art Paper 120gsm, Full Color",
          price: "Rp 1.000/pcs (min. 200)",
          time: "2-3 hari kerja"
        },
        {
          name: "Poster",
          specs: "A3, Art Paper 190gsm, Full Color",
          price: "Rp 5.000/pcs (min. 50)",
          time: "2-3 hari kerja"
        }
      ]
    },
    {
      icon: faBook,
      title: "Buku & Katalog",
      products: [
        {
          name: "Company Profile",
          specs: "A4, Art Paper 150gsm, Hard Cover",
          price: "Rp 75.000/pcs (min. 50)",
          time: "5-7 hari kerja"
        },
        {
          name: "Katalog Produk",
          specs: "A5, Art Paper 120gsm, Soft Cover",
          price: "Rp 35.000/pcs (min. 100)",
          time: "4-5 hari kerja"
        },
        {
          name: "Annual Report",
          specs: "A4, Art Paper 150gsm, Hard Cover",
          price: "Rp 100.000/pcs (min. 25)",
          time: "7-10 hari kerja"
        }
      ]
    },
    {
      icon: faBox,
      title: "Kemasan",
      products: [
        {
          name: "Box Packaging",
          specs: "Custom Size, Art Carton 310gsm",
          price: "Rp 8.000/pcs (min. 100)",
          time: "5-7 hari kerja"
        },
        {
          name: "Paper Bag",
          specs: "Custom Size, Art Paper 150gsm",
          price: "Rp 5.000/pcs (min. 200)",
          time: "4-5 hari kerja"
        },
        {
          name: "Label Produk",
          specs: "Custom Size, Sticker Vinyl",
          price: "Rp 2.000/pcs (min. 500)",
          time: "3-4 hari kerja"
        }
      ]
    },
    {
      icon: faTshirt,
      title: "Merchandise",
      products: [
        {
          name: "Kaos Custom",
          specs: "Cotton Combed 30s, Full Color",
          price: "Rp 85.000/pcs (min. 24)",
          time: "5-7 hari kerja"
        },
        {
          name: "Mug Custom",
          specs: "Ceramic, Full Color Print",
          price: "Rp 35.000/pcs (min. 24)",
          time: "3-4 hari kerja"
        },
        {
          name: "Tote Bag",
          specs: "Canvas, Full Color Print",
          price: "Rp 45.000/pcs (min. 50)",
          time: "4-5 hari kerja"
        }
      ]
    }
  ];

  const whyChooseUs = [
    {
      icon: faClock,
      title: "Pengerjaan Tepat Waktu",
      description: "Kami berkomitmen menyelesaikan pesanan sesuai deadline yang dijanjikan."
    },
    {
      icon: faComments,
      title: "Komunikasi Responsif",
      description: "Tim kami siap membantu dan merespon setiap pertanyaan dengan cepat."
    },
    {
      icon: faShieldAlt,
      title: "Garansi Kualitas",
      description: "Jaminan hasil cetak berkualitas atau uang kembali 100%."
    },
    {
      icon: faHeadset,
      title: "Dukungan 24/7",
      description: "Layanan pelanggan yang siap membantu kapanpun Anda butuhkan."
    }
  ];

  return (
    <div className="min-h-screen py-20 bg-gray-50 dark:bg-gray-900">
      {/* Header Section */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 pt-20 pb-12">
        <div className="text-center">
          <h1 className="text-4xl font-bold text-gray-900 dark:text-white sm:text-5xl">
            Layanan Kami
          </h1>
          <p className="mt-4 text-xl text-gray-600 dark:text-gray-300">
            Solusi percetakan profesional untuk mengembangkan bisnis Anda
          </p>
        </div>
      </div>

      {/* Main Services Section */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {mainServices.map((service, index) => (
            <div 
              key={index}
              className="bg-white dark:bg-gray-800 rounded-2xl shadow-custom dark:shadow-custom-dark overflow-hidden"
            >
              <div className="p-8">
                <div className="text-primary-500 mb-4">
                  <FontAwesomeIcon icon={service.icon} className="text-4xl" />
                </div>
                <h3 className="text-2xl font-bold text-gray-900 dark:text-white mb-4">
                  {service.title}
                </h3>
                <p className="text-gray-600 dark:text-gray-400 mb-6">
                  {service.description}
                </p>
                <ul className="space-y-3 mb-6">
                  {service.features.map((feature, idx) => (
                    <li key={idx} className="flex items-center text-gray-600 dark:text-gray-400">
                      <FontAwesomeIcon icon={faCheck} className="text-primary-500 mr-3 w-4 h-4" />
                      {feature}
                    </li>
                  ))}
                </ul>
                <div className="text-xl font-bold text-primary-600 dark:text-primary-400 mb-6">
                  {service.price}
                </div>
                {service.consultation && (
                  <a
                    href="/kontak"
                    className="block text-center bg-primary-500 hover:bg-primary-600 text-white font-medium px-6 py-3 rounded-lg transition-colors duration-200"
                  >
                    Konsultasi Gratis
                  </a>
                )}
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* Product Categories Section */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
        <div className="text-center mb-12">
          <h2 className="text-3xl font-bold text-gray-900 dark:text-white">
            Produk & Harga
          </h2>
          <p className="mt-4 text-lg text-gray-600 dark:text-gray-300">
            Berbagai pilihan produk cetak untuk kebutuhan bisnis Anda
          </p>
        </div>
        <div className="space-y-12">
          {productCategories.map((category, index) => (
            <div key={index} className="bg-white dark:bg-gray-800 rounded-2xl shadow-custom dark:shadow-custom-dark overflow-hidden">
              <div className="p-8">
                <div className="flex items-center mb-6">
                  <FontAwesomeIcon icon={category.icon} className="text-4xl text-primary-500 mr-4" />
                  <h3 className="text-2xl font-bold text-gray-900 dark:text-white">
                    {category.title}
                  </h3>
                </div>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                  {category.products.map((product, idx) => (
                    <div key={idx} className="bg-gray-50 dark:bg-gray-700 p-6 rounded-xl">
                      <h4 className="text-lg font-semibold text-gray-900 dark:text-white mb-2">
                        {product.name}
                      </h4>
                      <p className="text-sm text-gray-600 dark:text-gray-400 mb-2">
                        {product.specs}
                      </p>
                      <p className="text-primary-600 dark:text-primary-400 font-bold mb-2">
                        {product.price}
                      </p>
                      <p className="text-sm text-gray-500 dark:text-gray-400">
                        <FontAwesomeIcon icon={faClock} className="mr-2" />
                        {product.time}
                      </p>
                    </div>
                  ))}
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* Why Choose Us Section */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
        <div className="text-center mb-12">
          <h2 className="text-3xl font-bold text-gray-900 dark:text-white">
            Mengapa Memilih Kami
          </h2>
          <p className="mt-4 text-lg text-gray-600 dark:text-gray-300">
            Keunggulan layanan yang kami tawarkan untuk kepuasan Anda
          </p>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
          {whyChooseUs.map((item, index) => (
            <div key={index} className="text-center">
              <div className="text-primary-500 mb-4">
                <FontAwesomeIcon icon={item.icon} className="text-4xl" />
              </div>
              <h3 className="text-xl font-bold text-gray-900 dark:text-white mb-2">
                {item.title}
              </h3>
              <p className="text-gray-600 dark:text-gray-400">
                {item.description}
              </p>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
} 