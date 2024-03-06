import { city } from './city';
import { user } from './user';

export interface plan {
  id: number;
  phoneNo: number;
  mobileNo: number;
  mailId: string;
  cityId: number;
  street: string;
  launchDate: string;
  testimonials: string;
  description: string;
  userId: number;
  companyName: string;
  city: city;
  user: user;
}
