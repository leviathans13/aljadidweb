'use client';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { 
  faPrint, 
  faClock, 
  faHandshake, 
  faShieldAlt, 
  faTruck, 
  faCheckCircle 
} from '@fortawesome/free-solid-svg-icons';
import Blog from '../components/Blog';
import Portfolio from '../components/Portfolio';
import CTA from '../components/CTA';

export default function Home() {
  const features = [
    {
      icon: faPrint,
      title: "Teknologi Modern",
      description: "Menggunakan mesin cetak digital dan offset terkini dengan resolusi tinggi untuk hasil cetakan yang tajam dan presisi."
    },
    {
      icon: faShieldAlt,
      title: "Kualitas Premium",
      description: "Bahan berkualitas tinggi dan proses quality control ketat untuk memastikan hasil terbaik."
    },
    {
      icon: faClock,
      title: "Pengerjaan Cepat",
      description: "Estimasi waktu yang akurat dan pengerjaan tepat waktu untuk memenuhi deadline Anda."
    },
    {
      icon: faHandshake,
      title: "Layanan Prima",
      description: "Konsultasi gratis dan pendampingan dari tim profesional kami untuk hasil yang sesuai kebutuhan."
    },
    {
      icon: faTruck,
      title: "Pengiriman Cepat",
      description: "Layanan pengiriman yang aman dan tepat waktu ke seluruh wilayah Indonesia."
    },
    {
      icon: faCheckCircle,
      title: "Garansi Kepuasan",
      description: "Jaminan kualitas hasil cetak atau uang kembali 100% untuk kepuasan Anda."
    }
  ];

  const testimonials = [
    {
      name: "Ahmad Rizki",
      role: "Pemilik",
      company: "Toko Buku Cahaya",
      image: "https://images.pexels.com/photos/2379004/pexels-photo-2379004.jpeg?auto=compress&cs=tinysrgb&w=150",
      content: "Aljadid Printing selalu memberikan hasil cetakan yang berkualitas tinggi. Buku-buku yang kami cetak selalu mendapat pujian dari pelanggan kami."
    },
    {
      name: "Siti Aminah",
      role: "Marketing Manager",
      company: "Event Organizer Sukses",
      image: "https://images.pexels.com/photos/3796217/pexels-photo-3796217.jpeg?auto=compress&cs=tinysrgb&w=150",
      content: "Deadline yang ketat bukan masalah bagi Aljadid Printing. Mereka selalu bisa diandalkan untuk mencetak material promosi event kami tepat waktu."
    },
    {
      name: "Budi Prakoso",
      role: "Direktur",
      company: "PT Maju Jaya",
      image: "https://images.pexels.com/photos/2182970/pexels-photo-2182970.jpeg?auto=compress&cs=tinysrgb&w=150",
      content: "Kualitas cetakan dan layanan yang konsisten. Kami sudah bermitra dengan Aljadid Printing selama 5 tahun dan tidak pernah kecewa."
    }
  ];

  return (
    <div className="min-h-screen">
      {/* Hero Section */}
      <section className="relative h-screen flex items-center justify-center text-center text-white">
        <div 
          className="absolute inset-0 z-0"
          style={{
            backgroundImage: 'url("https://images.pexels.com/photos/6147369/pexels-photo-6147369.jpeg?auto=compress&cs=tinysrgb&w=1920")',
            backgroundSize: 'cover',
            backgroundPosition: 'center',
          }}
        >
          <div className="absolute inset-0 bg-gray-900 opacity-75"></div>
        </div>
        
        <div className="relative z-10 max-w-4xl mx-auto px-4 sm:px-6 lg:px-8">
          <h1 className="text-4xl sm:text-5xl lg:text-6xl font-bold mb-6">
            Contoh Website, Buat aja. Harga Terjangkau Bang!!!
          </h1>
          <p className="text-xl sm:text-2xl mb-2 text-gray-300">
            Dibuat dengan NextJs dan React Ts,
          </p>
          <p className="text-xl sm:text-2xl mb-2 text-gray-300">
            Tampilan dijamin Modern (Bukan PHP).
          </p>
          <p className="text-xl sm:text-2xl mb-2 text-gray-300">
            Buat Website Anda dengan mudah dan cepat
          </p>
          <p className="text-xl sm:text-2xl mb-8 text-gray-300">
            Bersama saya, Iki dudu template boss!
          </p>
          <div className="flex flex-col sm:flex-row gap-4 justify-center">
            <a 
              href="/layanan"
              className="inline-flex items-center px-8 py-3 border border-transparent text-base font-medium rounded-md text-white bg-primary-600 hover:bg-primary-700 transition-colors duration-200"
            >
              Lihat Layanan
            </a>
            <a 
              href="/kontak"
              className="inline-flex items-center px-8 py-3 border-2 border-white text-base font-medium rounded-md text-white hover:bg-white hover:text-primary-600 transition-colors duration-200"
            >
              Konsultasi Gratis
            </a>
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section className="py-20 bg-gray-50 dark:bg-gray-800" id="layanan">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center mb-16">
            <h2 className="text-3xl font-bold text-gray-900 dark:text-white sm:text-4xl">
              Keunggulan Aljadid Printing
            </h2>
            <p className="mt-4 text-lg text-gray-600 dark:text-gray-300">
              Komitmen kami untuk memberikan layanan percetakan terbaik dengan standar profesional
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {features.map((feature, index) => (
              <div key={index} className="bg-white dark:bg-gray-700 p-8 rounded-2xl shadow-custom dark:shadow-custom-dark">
                <div className="text-primary-500 mb-4">
                  <FontAwesomeIcon icon={feature.icon} className="text-4xl" />
                </div>
                <h3 className="text-xl font-bold text-gray-900 dark:text-white mb-4">
                  {feature.title}
                </h3>
                <p className="text-gray-600 dark:text-gray-300">
                  {feature.description}
                </p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Portfolio Section */}
      <section className="py-20">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center mb-16">
            <h2 className="text-3xl font-bold text-gray-900 dark:text-white sm:text-4xl">
              Hasil Cetakan Kami
            </h2>
            <p className="mt-4 text-lg text-gray-600 dark:text-gray-300">
              Beberapa contoh proyek percetakan yang telah kami kerjakan dengan kualitas premium
            </p>
          </div>
          <Portfolio />
          <div className="text-center mt-12">
            <a
              href="/portfolio"
              className="inline-flex items-center px-8 py-3 border border-primary-500 text-base font-medium rounded-md text-primary-600 hover:bg-primary-50 dark:hover:bg-primary-900/20 transition-colors duration-200"
            >
              Lihat Semua Portfolio
            </a>
          </div>
        </div>
      </section>

      {/* Testimonials Section */}
      <section className="py-20 bg-gray-50 dark:bg-gray-800">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center mb-16">
            <h2 className="text-3xl font-bold text-gray-900 dark:text-white sm:text-4xl">
              Testimoni Klien
            </h2>
            <p className="mt-4 text-lg text-gray-600 dark:text-gray-300">
              Apa kata mereka tentang kualitas cetakan dan layanan kami
            </p>
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {testimonials.map((testimonial, index) => (
              <div 
                key={index}
                className="bg-white dark:bg-gray-700 p-8 rounded-2xl shadow-custom dark:shadow-custom-dark"
              >
                <div className="flex items-center mb-6">
                  <img
                    src={testimonial.image}
                    alt={testimonial.name}
                    className="w-16 h-16 rounded-full object-cover"
                  />
                  <div className="ml-4">
                    <h3 className="text-lg font-semibold text-gray-900 dark:text-white">
                      {testimonial.name}
                    </h3>
                    <p className="text-sm text-gray-500 dark:text-gray-400">
                      {testimonial.role} di {testimonial.company}
                    </p>
                  </div>
                </div>
                <p className="text-gray-600 dark:text-gray-300 italic">
                  &ldquo;{testimonial.content}&rdquo;
                </p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Blog Section */}
      <section className="py-20">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center mb-16">
            <h2 className="text-3xl font-bold text-gray-900 dark:text-white sm:text-4xl">
              Tips & Artikel
            </h2>
            <p className="mt-4 text-lg text-gray-600 dark:text-gray-300">
              Informasi dan panduan seputar dunia percetakan untuk bisnis Anda
            </p>
          </div>
          <Blog />
          <div className="text-center mt-12">
            <a
              href="/blog"
              className="inline-flex items-center px-8 py-3 border border-primary-500 text-base font-medium rounded-md text-primary-600 hover:bg-primary-50 dark:hover:bg-primary-900/20 transition-colors duration-200"
            >
              Lihat Semua Artikel
            </a>
          </div>
        </div>
      </section>

      {/* CTA Section */}
      <CTA />
    </div>
  );
}
