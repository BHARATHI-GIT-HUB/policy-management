import { city } from './city';
import { user } from './user';
export interface Provider {
  id: number;
  phoneNo: number;
  moblieNo: string;
  mailId: string;
  cityId: number;
  street: string;
  launchDate: string;
  testimonials: string | null;
  description?: string | null;
  companyName?: string | null;
  userId: number;
  user: user;
  city: city;
}
