'use client';

import Link from 'next/link';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { 
  faFacebookF, 
  faTwitter, 
  faInstagram, 
  faLinkedinIn 
} from '@fortawesome/free-brands-svg-icons';
import { 
  faMapMarkerAlt, 
  faPhone, 
  faEnvelope 
} from '@fortawesome/free-solid-svg-icons';

const Footer = () => {
  const currentYear = new Date().getFullYear();
  
  return (
    <footer className="bg-gray-900 text-gray-300">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
          {/* Company Info */}
          <div>
            <h3 className="text-white text-lg font-bold mb-4">Aljadid Web</h3>
            <p className="text-sm mb-4">
              Solusi digital terpercaya untuk mengembangkan bisnis Anda di era modern. 
              Kami fokus pada kualitas, inovasi, dan kepuasan klien.
            </p>
            <div className="flex space-x-4">
              <a href="https://facebook.com" className="hover:text-primary-400 transition-colors">
                <FontAwesomeIcon icon={faFacebookF} />
              </a>
              <a href="https://twitter.com" className="hover:text-primary-400 transition-colors">
                <FontAwesomeIcon icon={faTwitter} />
              </a>
              <a href="https://instagram.com" className="hover:text-primary-400 transition-colors">
                <FontAwesomeIcon icon={faInstagram} />
              </a>
              <a href="https://linkedin.com" className="hover:text-primary-400 transition-colors">
                <FontAwesomeIcon icon={faLinkedinIn} />
              </a>
            </div>
          </div>

          {/* Quick Links */}
          <div>
            <h3 className="text-white text-lg font-bold mb-4">Link Cepat</h3>
            <ul className="space-y-2">
              <li>
                <Link href="/layanan" className="hover:text-primary-400 transition-colors">
                  Layanan
                </Link>
              </li>
              <li>
                <Link href="/portfolio" className="hover:text-primary-400 transition-colors">
                  Portfolio
                </Link>
              </li>
              <li>
                <Link href="/blog" className="hover:text-primary-400 transition-colors">
                  Blog
                </Link>
              </li>
              <li>
                <Link href="/kontak" className="hover:text-primary-400 transition-colors">
                  Kontak
                </Link>
              </li>
            </ul>
          </div>

          {/* Services */}
          <div>
            <h3 className="text-white text-lg font-bold mb-4">Layanan</h3>
            <ul className="space-y-2">
              <li>
                <Link href="/layanan/web-development" className="hover:text-primary-400 transition-colors">
                  Web Development
                </Link>
              </li>
              <li>
                <Link href="/layanan/ui-ux-design" className="hover:text-primary-400 transition-colors">
                  UI/UX Design
                </Link>
              </li>
              <li>
                <Link href="/layanan/digital-marketing" className="hover:text-primary-400 transition-colors">
                  Digital Marketing
                </Link>
              </li>
              <li>
                <Link href="/layanan/maintenance" className="hover:text-primary-400 transition-colors">
                  Maintenance & Support
                </Link>
              </li>
            </ul>
          </div>

          {/* Contact Info */}
          <div>
            <h3 className="text-white text-lg font-bold mb-4">Kontak</h3>
            <ul className="space-y-4">
              <li className="flex items-start">
                <FontAwesomeIcon icon={faMapMarkerAlt} className="mt-1 mr-3 text-primary-400" />
                <span>
                  Jl. Raya Utama No. 123<br />
                  Jakarta Selatan, 12345
                </span>
              </li>
              <li className="flex items-center">
                <FontAwesomeIcon icon={faPhone} className="mr-3 text-primary-400" />
                <a href="tel:+6281234567890" className="hover:text-primary-400 transition-colors">
                  +62 812-3456-7890
                </a>
              </li>
              <li className="flex items-center">
                <FontAwesomeIcon icon={faEnvelope} className="mr-3 text-primary-400" />
                <a href="mailto:info@aljadidweb.com" className="hover:text-primary-400 transition-colors">
                  info@aljadidweb.com
                </a>
              </li>
            </ul>
          </div>
        </div>

        {/* Bottom Bar */}
        <div className="border-t border-gray-800 mt-12 pt-8 text-sm text-center">
          <p>
            © {currentYear} Aljadid Web. All rights reserved.
          </p>
        </div>
      </div>
    </footer>
  );
};

export default Footer; 