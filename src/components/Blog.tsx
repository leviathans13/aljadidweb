'use client';

import Link from 'next/link';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faClock, faUser } from '@fortawesome/free-solid-svg-icons';

interface BlogPostProps {
  title: string;
  excerpt: string;
  image: string;
  author: string;
  date: string;
  category: string;
  slug: string;
}

const BlogPost = ({ title, excerpt, image, author, date, category, slug }: BlogPostProps) => {
  return (
    <div className="bg-white dark:bg-gray-800 rounded-2xl shadow-custom dark:shadow-custom-dark overflow-hidden group">
      <div className="relative overflow-hidden aspect-video">
        <img 
          src={image} 
          alt={title}
          className="w-full h-full object-cover transform transition-transform duration-500 group-hover:scale-110"
        />
        <div className="absolute top-4 left-4">
          <span className="px-3 py-1 bg-primary-500 text-white text-sm rounded-full">
            {category}
          </span>
        </div>
      </div>
      
      <div className="p-6">
        <div className="flex items-center text-sm text-gray-500 dark:text-gray-400 mb-4 space-x-4">
          <div className="flex items-center">
            <FontAwesomeIcon icon={faUser} className="mr-2" />
            <span>{author}</span>
          </div>
          <div className="flex items-center">
            <FontAwesomeIcon icon={faClock} className="mr-2" />
            <span>{date}</span>
          </div>
        </div>
        
        <h3 className="text-xl font-bold mb-2 text-gray-900 dark:text-white group-hover:text-primary-500 dark:group-hover:text-primary-400 transition-colors">
          <Link href={`/blog/${slug}`} aria-label={`Baca artikel ${title}`}>
            {title}
          </Link>
        </h3>
        
        <p className="text-gray-600 dark:text-gray-400 text-sm mb-4">
          {excerpt}
        </p>
        
        <Link 
          href={`/blog/${slug}`}
          className="inline-block text-primary-500 dark:text-primary-400 hover:text-primary-600 dark:hover:text-primary-300 transition-colors text-sm font-medium"
          aria-label={`Baca selengkapnya tentang ${title}`}
        >
          Baca Selengkapnya →
        </Link>
      </div>
    </div>
  );
};

const Blog = () => {
  const posts = [
    {
      title: "5 Jenis Kertas Terbaik untuk Katalog Produk",
      excerpt: "Panduan lengkap memilih jenis kertas yang tepat untuk katalog produk Anda, dari art paper hingga matt paper.",
      image: "https://images.pexels.com/photos/5708064/pexels-photo-5708064.jpeg?auto=compress&cs=tinysrgb&w=800",
      author: "Ahmad Rizki",
      date: "21 Des 2023",
      category: "Tips Cetak",
      slug: "jenis-kertas-katalog"
    },
    {
      title: "Teknik Finishing Cetak Modern",
      excerpt: "Eksplorasi berbagai teknik finishing modern yang bisa membuat hasil cetakan Anda lebih menarik dan bernilai.",
      image: "https://images.pexels.com/photos/5708069/pexels-photo-5708069.jpeg?auto=compress&cs=tinysrgb&w=800",
      author: "Siti Rahma",
      date: "18 Des 2023",
      category: "Teknologi",
      slug: "teknik-finishing-modern"
    },
    {
      title: "Panduan Desain Kemasan yang Menjual",
      excerpt: "Tips dan trik merancang desain kemasan yang tidak hanya menarik tapi juga efektif dalam meningkatkan penjualan.",
      image: "https://images.pexels.com/photos/5709661/pexels-photo-5709661.jpeg?auto=compress&cs=tinysrgb&w=800",
      author: "Budi Santoso",
      date: "15 Des 2023",
      category: "Desain",
      slug: "desain-kemasan-menjual"
    }
  ];

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 sm:gap-8">
      {posts.map((post, index: number) => (
        <BlogPost key={index} {...post} />
      ))}
    </div>
  );
};

export default Blog; 