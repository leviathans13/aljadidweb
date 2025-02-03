'use client';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPrint, faPhone, faEnvelope, faClock } from '@fortawesome/free-solid-svg-icons';

const CTA = () => {
  return (
    <section className="py-20 bg-primary-500 dark:bg-primary-600">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-12 items-center">
          <div>
            <h2 className="text-3xl font-bold text-white mb-6">
              Siap Mencetak Proyek Anda?
            </h2>
            <p className="text-primary-100 text-lg mb-8">
              Konsultasikan kebutuhan cetak Anda dengan tim ahli kami. Dapatkan penawaran terbaik dan hasil cetakan berkualitas untuk bisnis Anda.
            </p>
            <div className="space-y-4">
              <div className="flex items-center text-white">
                <FontAwesomeIcon icon={faPrint} className="w-5 h-5 mr-3" />
                <span>Hasil Cetak Premium</span>
              </div>
              <div className="flex items-center text-white">
                <FontAwesomeIcon icon={faClock} className="w-5 h-5 mr-3" />
                <span>Pengerjaan Cepat</span>
              </div>
              <div className="flex items-center text-white">
                <FontAwesomeIcon icon={faPhone} className="w-5 h-5 mr-3" />
                <span>Konsultasi Gratis</span>
              </div>
            </div>
            <div className="mt-8 space-y-4 sm:space-y-0 sm:space-x-4 flex flex-col sm:flex-row">
              <a
                href="/kontak"
                className="inline-flex items-center justify-center px-8 py-3 border border-transparent text-base font-medium rounded-md text-primary-600 bg-white hover:bg-primary-50 transition-colors duration-200"
              >
                Hubungi Kami
              </a>
              <a
                href="https://wa.me/6281234567890"
                target="_blank"
                rel="noopener noreferrer"
                className="inline-flex items-center justify-center px-8 py-3 border-2 border-white text-base font-medium rounded-md text-white hover:bg-white hover:text-primary-600 transition-colors duration-200"
              >
                Chat WhatsApp
              </a>
            </div>
          </div>
          <div className="space-y-6 lg:pl-12">
            <div className="bg-white/10 p-6 rounded-lg backdrop-blur-sm">
              <h3 className="text-xl font-semibold text-white mb-4">
                Hubungi Kami
              </h3>
              <div className="space-y-4">
                <div className="flex items-center text-white">
                  <FontAwesomeIcon icon={faPhone} className="w-5 h-5 mr-3" />
                  <a href="tel:+6281234567890" className="hover:text-primary-200">
                    +62 812-3456-7890
                  </a>
                </div>
                <div className="flex items-center text-white">
                  <FontAwesomeIcon icon={faEnvelope} className="w-5 h-5 mr-3" />
                  <a href="mailto:info@aljadidprinting.com" className="hover:text-primary-200">
                    info@aljadidprinting.com
                  </a>
                </div>
                <div className="flex items-center text-white">
                  <FontAwesomeIcon icon={faClock} className="w-5 h-5 mr-3" />
                  <span>Senin - Sabtu: 08:00 - 17:00</span>
                </div>
              </div>
            </div>
            <div className="bg-white/10 p-6 rounded-lg backdrop-blur-sm">
              <h3 className="text-xl font-semibold text-white mb-4">
                Promo Spesial
              </h3>
              <p className="text-primary-100 mb-4">
                Dapatkan diskon 20% untuk pemesanan pertama Anda. Berlaku untuk semua jenis cetakan.
              </p>
              <p className="text-white font-medium">
                Kode: ALJADID20
              </p>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default CTA; 