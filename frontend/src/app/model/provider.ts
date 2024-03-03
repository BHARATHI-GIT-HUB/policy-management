export interface provider {
  id: number;
  Name: string;
  phoneNo: string | null;
  mobileNo?: string | null;
  mailId?: string | null;
  cityId: number;
  street?: string | null;
  launchDate: string; // Assuming DateOnly is represented as a string
  testimonials?: string | null;
  description?: string | null;
  companyName?: string | null;
}
