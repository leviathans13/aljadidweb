'use client';

import { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCalendar, faUser, faTag, faArrowRight } from '@fortawesome/free-solid-svg-icons';

export default function Blog() {
  const categories = ['Semua', 'Tips & Trik', 'Teknologi', 'Desain', 'Industri'];
  const [activeCategory, setActiveCategory] = useState('Semua');

  const blogPosts = [
    {
      title: "5 Jenis Kertas Terbaik untuk Katalog Produk",
      excerpt: "Panduan lengkap memilih jenis kertas yang tepat untuk katalog produk Anda, dari art paper hingga matt paper.",
      image: "https://source.unsplash.com/random/800x600/?paper",
      category: "Tips & Trik",
      author: "Ahmad Fadli",
      date: "15 Mar 2024",
      readTime: "5 min"
    },
    {
      title: "Teknik Finishing Cetak Modern",
      excerpt: "Mengenal berbagai teknik finishing modern seperti spot UV, hot stamping, dan emboss yang bisa membuat hasil cetak lebih menarik.",
      image: "https://source.unsplash.com/random/800x600/?printing",
      category: "Teknologi",
      author: "Siti Rahma",
      date: "12 Mar 2024",
      readTime: "7 min"
    },
    {
      title: "Panduan Desain Kemasan yang Menjual",
      excerpt: "Tips dan trik merancang desain kemasan yang tidak hanya menarik tapi juga efektif dalam meningkatkan penjualan produk.",
      image: "https://source.unsplash.com/random/800x600/?packaging",
      category: "Desain",
      author: "Budi Santoso",
      date: "10 Mar 2024",
      readTime: "6 min"
    },
    {
      title: "Tren Industri Percetakan 2024",
      excerpt: "Mengulas tren terbaru dalam industri percetakan, dari teknologi digital printing hingga material ramah lingkungan.",
      image: "https://source.unsplash.com/random/800x600/?industry",
      category: "Industri",
      author: "Diana Putri",
      date: "8 Mar 2024",
      readTime: "8 min"
    },
    {
      title: "Cara Memilih Jasa Percetakan yang Tepat",
      excerpt: "Panduan lengkap memilih jasa percetakan yang sesuai dengan kebutuhan bisnis Anda, mulai dari kualitas hingga harga.",
      image: "https://source.unsplash.com/random/800x600/?print-shop",
      category: "Tips & Trik",
      author: "Rudi Hartono",
      date: "5 Mar 2024",
      readTime: "6 min"
    },
    {
      title: "Inovasi Teknologi dalam Dunia Cetak",
      excerpt: "Menjelajahi berbagai inovasi teknologi terbaru yang mengubah industri percetakan menjadi lebih efisien dan berkualitas.",
      image: "https://source.unsplash.com/random/800x600/?technology",
      category: "Teknologi",
      author: "Maya Sari",
      date: "3 Mar 2024",
      readTime: "7 min"
    }
  ];

  const filteredPosts = activeCategory === 'Semua'
    ? blogPosts
    : blogPosts.filter(post => post.category === activeCategory);

  return (
    <div className="min-h-screen py-20 bg-gray-50 dark:bg-gray-900">
      {/* Header Section */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 pt-20 pb-12">
        <div className="text-center">
          <h1 className="text-4xl font-bold text-gray-900 dark:text-white sm:text-5xl">
            Blog & Artikel
          </h1>
          <p className="mt-4 text-xl text-gray-600 dark:text-gray-300">
            Informasi dan tips seputar dunia percetakan
          </p>
        </div>
      </div>

      {/* Category Filter */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 mb-12">
        <div className="flex flex-wrap justify-center gap-4">
          {categories.map((category) => (
            <button
              key={category}
              onClick={() => setActiveCategory(category)}
              className={`px-6 py-2 rounded-full transition-colors duration-200 ${
                activeCategory === category
                  ? 'bg-primary-500 text-white'
                  : 'bg-white dark:bg-gray-800 text-gray-600 dark:text-gray-300 hover:bg-primary-50 dark:hover:bg-gray-700'
              }`}
            >
              {category}
            </button>
          ))}
        </div>
      </div>

      {/* Blog Grid */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {filteredPosts.map((post, index) => (
            <article
              key={index}
              className="bg-white dark:bg-gray-800 rounded-2xl shadow-custom dark:shadow-custom-dark overflow-hidden group"
            >
              <div className="relative overflow-hidden aspect-video">
                <img
                  src={post.image}
                  alt={post.title}
                  className="w-full h-full object-cover transform transition-transform duration-500 group-hover:scale-110"
                />
                <div className="absolute top-4 left-4">
                  <span className="px-3 py-1 bg-primary-500 text-white text-sm rounded-full">
                    {post.category}
                  </span>
                </div>
              </div>

              <div className="p-6">
                <div className="flex items-center gap-4 text-sm text-gray-500 dark:text-gray-400 mb-4">
                  <div className="flex items-center gap-2">
                    <FontAwesomeIcon icon={faCalendar} className="w-4 h-4" />
                    <span>{post.date}</span>
                  </div>
                  <div className="flex items-center gap-2">
                    <FontAwesomeIcon icon={faUser} className="w-4 h-4" />
                    <span>{post.author}</span>
                  </div>
                  <div className="flex items-center gap-2">
                    <FontAwesomeIcon icon={faTag} className="w-4 h-4" />
                    <span>{post.readTime}</span>
                  </div>
                </div>

                <h2 className="text-xl font-bold text-gray-900 dark:text-white mb-3">
                  {post.title}
                </h2>
                <p className="text-gray-600 dark:text-gray-400 text-sm mb-4">
                  {post.excerpt}
                </p>

                <a
                  href="#"
                  className="inline-flex items-center text-primary-500 hover:text-primary-600 dark:hover:text-primary-400 transition-colors"
                >
                  <span className="mr-2">Baca Selengkapnya</span>
                  <FontAwesomeIcon icon={faArrowRight} className="w-4 h-4" />
                </a>
              </div>
            </article>
          ))}
        </div>
      </div>

      {/* Newsletter Section */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-20">
        <div className="bg-primary-500 dark:bg-primary-600 rounded-2xl p-8 md:p-12">
          <div className="max-w-2xl mx-auto text-center">
            <h2 className="text-3xl font-bold text-white mb-4">
              Dapatkan Update Terbaru
            </h2>
            <p className="text-primary-100 mb-8">
              Berlangganan newsletter kami untuk mendapatkan artikel dan tips terbaru seputar dunia percetakan
            </p>
            <form className="flex flex-col sm:flex-row gap-4 max-w-md mx-auto">
              <input
                type="email"
                placeholder="Masukkan email Anda"
                className="flex-1 px-4 py-3 rounded-lg focus:ring-2 focus:ring-white"
              />
              <button
                type="submit"
                className="px-6 py-3 bg-white text-primary-500 font-medium rounded-lg hover:bg-primary-50 transition-colors"
              >
                Berlangganan
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
} 