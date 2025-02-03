'use client';

import { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faExternalLinkAlt } from '@fortawesome/free-solid-svg-icons';

interface PortfolioItemProps {
  title: string;
  description: string;
  image: string;
  tags: string[];
  slug: string;
}

const PortfolioItem = ({ title, description, image, tags, slug }: PortfolioItemProps) => {
  const [isHovered, setIsHovered] = useState(false);

  return (
    <div 
      className="relative group overflow-hidden rounded-2xl"
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <img 
        src={image} 
        alt={title}
        className="w-full h-64 object-cover transform transition-transform duration-500 group-hover:scale-110"
      />
      
      <div className={`absolute inset-0 bg-gradient-to-t from-gray-900 via-gray-900/70 to-transparent transition-opacity duration-300 ${isHovered ? 'opacity-100' : 'opacity-0'}`}>
        <div className="absolute bottom-0 left-0 right-0 p-6">
          <h3 className="text-white font-bold text-xl mb-2">{title}</h3>
          <p className="text-gray-200 text-sm mb-4">{description}</p>
          
          <div className="flex flex-wrap gap-2 mb-4">
            {tags.map((tag, index: number) => (
              <span 
                key={index}
                className="text-xs px-3 py-1 bg-primary-500/20 text-primary-300 rounded-full"
              >
                {tag}
              </span>
            ))}
          </div>
          
          <a 
            href={`/portfolio/${slug}`}
            className="inline-flex items-center text-primary-400 hover:text-primary-300 transition-colors"
            aria-label={`Lihat detail proyek ${title}`}
          >
            <span className="mr-2">Lihat Detail</span>
            <FontAwesomeIcon icon={faExternalLinkAlt} className="text-sm" />
          </a>
        </div>
      </div>
    </div>
  );
};

const Portfolio = () => {
  const projects = [
    {
      title: "Katalog Produk Premium",
      description: "Katalog full color 40 halaman dengan finishing glossy untuk brand fashion",
      image: "https://images.pexels.com/photos/5691698/pexels-photo-5691698.jpeg?auto=compress&cs=tinysrgb&w=800",
      tags: ["Cetak Offset", "Full Color", "Finishing Glossy"],
      slug: "katalog-produk-premium"
    },
    {
      title: "Packaging Makanan",
      description: "Desain dan cetak kemasan untuk produk makanan premium",
      image: "https://images.pexels.com/photos/7262897/pexels-photo-7262897.jpeg?auto=compress&cs=tinysrgb&w=800",
      tags: ["Packaging", "Die-cut", "Food Grade"],
      slug: "packaging-makanan"
    },
    {
      title: "Company Profile",
      description: "Buku company profile eksklusif dengan hard cover dan hot print",
      image: "https://images.pexels.com/photos/5709661/pexels-photo-5709661.jpeg?auto=compress&cs=tinysrgb&w=800",
      tags: ["Hard Cover", "Hot Print", "Premium"],
      slug: "company-profile"
    }
  ];

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 sm:gap-8">
      {projects.map((project, index: number) => (
        <PortfolioItem key={index} {...project} />
      ))}
    </div>
  );
};

export default Portfolio; 