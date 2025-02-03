'use client';

import { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faExternalLinkAlt } from '@fortawesome/free-solid-svg-icons';

export default function Portfolio() {
  const categories = ['Semua', 'Buku', 'Kemasan', 'Marketing Kit', 'Merchandise'];
  const [activeCategory, setActiveCategory] = useState('Semua');

  const portfolioItems = [
    {
      title: "Annual Report PT Maju Bersama 2023",
      description: "Laporan tahunan 120 halaman dengan finishing hard cover dan hot print gold.",
      image: "https://source.unsplash.com/random/800x600/?annual-report",
      category: "Buku",
      tags: ["Hard Cover", "Hot Print", "Premium Paper"],
      client: "PT Maju Bersama"
    },
    {
      title: "Kemasan Premium Kue Lebaran",
      description: "Desain dan produksi kemasan premium untuk produk kue lebaran dengan finishing glossy dan spot UV.",
      image: "https://source.unsplash.com/random/800x600/?packaging-box",
      category: "Kemasan",
      tags: ["Die-cut", "Spot UV", "Food Grade"],
      client: "Sweet Bakery"
    },
    {
      title: "Marketing Kit Bank Sukses",
      description: "Paket marketing kit lengkap termasuk brosur, folder, dan kartu nama dengan desain premium.",
      image: "https://source.unsplash.com/random/800x600/?brochure",
      category: "Marketing Kit",
      tags: ["Corporate Identity", "Premium Print", "Full Color"],
      client: "Bank Sukses"
    },
    {
      title: "Kalender Meja Custom 2024",
      description: "Kalender meja eksklusif dengan desain custom dan finishing special paper.",
      image: "https://source.unsplash.com/random/800x600/?calendar",
      category: "Merchandise",
      tags: ["Custom Design", "Special Paper", "Wire-O"],
      client: "Corporate Group"
    },
    {
      title: "Company Profile Startup Tech",
      description: "Buku company profile 40 halaman dengan konsep modern dan teknologi.",
      image: "https://source.unsplash.com/random/800x600/?company-profile",
      category: "Buku",
      tags: ["Modern Design", "Soft Cover", "Matt Paper"],
      client: "Startup Tech Indonesia"
    },
    {
      title: "Packaging Produk Kosmetik",
      description: "Seri kemasan produk kosmetik dengan finishing mewah dan ramah lingkungan.",
      image: "https://source.unsplash.com/random/800x600/?cosmetic-packaging",
      category: "Kemasan",
      tags: ["Eco Friendly", "Luxury Finish", "Custom Shape"],
      client: "Beauty Care Co"
    }
  ];

  const filteredItems = activeCategory === 'Semua' 
    ? portfolioItems 
    : portfolioItems.filter(item => item.category === activeCategory);

  return (
    <div className="min-h-screen py-20 bg-gray-50 dark:bg-gray-900">
      {/* Header Section */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 pt-20 pb-12">
        <div className="text-center">
          <h1 className="text-4xl font-bold text-gray-900 dark:text-white sm:text-5xl">
            Portfolio Kami
          </h1>
          <p className="mt-4 text-xl text-gray-600 dark:text-gray-300">
            Hasil cetakan berkualitas untuk berbagai kebutuhan bisnis
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

      {/* Portfolio Grid */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {filteredItems.map((item, index) => (
            <div
              key={index}
              className="bg-white dark:bg-gray-800 rounded-2xl shadow-custom dark:shadow-custom-dark overflow-hidden group"
            >
              <div className="relative overflow-hidden aspect-video">
                <img
                  src={item.image}
                  alt={item.title}
                  className="w-full h-full object-cover transform transition-transform duration-500 group-hover:scale-110"
                />
                <div className="absolute top-4 left-4">
                  <span className="px-3 py-1 bg-primary-500 text-white text-sm rounded-full">
                    {item.category}
                  </span>
                </div>
              </div>

              <div className="p-6">
                <h3 className="text-xl font-bold text-gray-900 dark:text-white mb-2">
                  {item.title}
                </h3>
                <p className="text-gray-600 dark:text-gray-400 text-sm mb-4">
                  {item.description}
                </p>

                <div className="flex flex-wrap gap-2 mb-4">
                  {item.tags.map((tag, idx) => (
                    <span
                      key={idx}
                      className="text-xs px-3 py-1 bg-primary-50 dark:bg-primary-900/20 text-primary-600 dark:text-primary-400 rounded-full"
                    >
                      {tag}
                    </span>
                  ))}
                </div>

                <div className="flex items-center justify-between">
                  <span className="text-sm text-gray-500 dark:text-gray-400">
                    {item.client}
                  </span>
                  <button className="text-primary-500 hover:text-primary-600 dark:hover:text-primary-400 transition-colors">
                    <FontAwesomeIcon icon={faExternalLinkAlt} className="w-5 h-5" />
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* CTA Section */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-20">
        <div className="text-center">
          <h2 className="text-3xl font-bold text-gray-900 dark:text-white mb-4">
            Siap Mencetak Proyek Anda?
          </h2>
          <p className="text-lg text-gray-600 dark:text-gray-300 mb-8">
            Konsultasikan kebutuhan cetak Anda dengan tim ahli kami
          </p>
          <a
            href="/kontak"
            className="inline-flex items-center px-8 py-3 border border-transparent text-base font-medium rounded-md text-white bg-primary-600 hover:bg-primary-700 transition-colors duration-200"
          >
            Mulai Konsultasi
          </a>
        </div>
      </div>
    </div>
  );
} 