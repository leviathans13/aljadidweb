import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faQuoteLeft } from '@fortawesome/free-solid-svg-icons';

interface TestimonialProps {
  name: string;
  role: string;
  company: string;
  image: string;
  content: string;
}

const Testimonial = ({ name, role, company, image, content }: TestimonialProps) => {
  return (
    <div className="bg-white dark:bg-gray-800 p-6 sm:p-8 rounded-2xl shadow-custom dark:shadow-custom-dark">
      <div className="flex flex-col h-full">
        <div className="mb-6 text-primary-500 dark:text-primary-400">
          <FontAwesomeIcon icon={faQuoteLeft} className="text-3xl sm:text-4xl opacity-50" />
        </div>
        
        <p className="text-gray-600 dark:text-gray-400 mb-8 flex-grow text-sm sm:text-base italic">
          "{content}"
        </p>
        
        <div className="flex items-center">
          <img 
            src={image} 
            alt={name}
            className="w-12 h-12 rounded-full object-cover mr-4"
          />
          <div>
            <h4 className="font-bold text-gray-900 dark:text-white text-sm sm:text-base">
              {name}
            </h4>
            <p className="text-gray-600 dark:text-gray-400 text-sm">
              {role} - {company}
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Testimonial; 