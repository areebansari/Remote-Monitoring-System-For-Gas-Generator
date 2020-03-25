// filters.c
//-------------------
// median_filter:
// This function sorts an array of longs so it's in
// ascending order. It then returns the middle element.
// ie., If the array has 7 elements (0-6), it returns
// the element at index 3.  The user should ensure that
// the array has an odd number of elements.
// This function stores data from prior calls in a static
// buffer.

// The output of this function will always be N/2 +1
// elements behind the input data, where N is the filter width.

//-----------------------------------------------------
void Insertion_Sort_16(int16 *data, char array_size)
{
   char i, j;
   int16 index;

   for(i = 1; i < array_size; i++)
   {
      index = data[i];
      j = i;

      while ((j > 0) && (data[j-1] > index))
      {
         data[j] = data[j-1];
         j = j - 1;
      }

      data[j] = index;
   }
}

int16 median_filter(int16 latest_element)
{
   static int16 input_buffer[MEDIAN_FILTER_WIDTH];
   static char inbuf_index = 0;
   static char num_elements = 0;
   int16 sorted_data[MEDIAN_FILTER_WIDTH];
   int16 median;

   // Insert incoming data element into circular input buffer.
   input_buffer[inbuf_index] = latest_element;

   #ifdef DISPLAY_INPUT_BUFFER_ARRAY
   int b=0;
   
   fprintf(PC,"\r\ninbuf_index : %u\r\n",inbuf_index);
   
   for (b=0; b<MEDIAN_FILTER_WIDTH; b++)
   {
      fprintf(PC,"input_buffer[%u] : %Lu\r\n", b, input_buffer[b]);
   }
   fprintf(Pc,"\r\n------------------------------------------\r\n");
   
   #endif

   inbuf_index++;

   if(inbuf_index >= MEDIAN_FILTER_WIDTH)  // If index went past buffer end
   inbuf_index = 0;       // then reset it to start of buffer

   if(num_elements < MEDIAN_FILTER_WIDTH)
   num_elements++;

   // THIS LINE MAY NOT BE NEEDED IF SORTED DATA IS STATIC.
   memset(sorted_data, 0, MEDIAN_FILTER_WIDTH * 2);

   // Copy input data buffer to the (to be) sorted data array.
   memcpy(sorted_data, input_buffer, num_elements * 2);   // memcpy works on bytes

   // Then sort the data.
   Insertion_Sort_16(sorted_data, MEDIAN_FILTER_WIDTH);

   #ifdef DISPLAY_SORTED_ARRAY
   int p=0;
   for (p=0; p<MEDIAN_FILTER_WIDTH; p++)
   {
      fprintf(PC,"sorted_data[%u] : %Lu\r\n", p, sorted_data[p]);
   }
   
   #endif

   // During the first few calls to this function, we have fewer
   // elements in the sorted data array than the filter width.
   // So to compensate for that, we pick the median from the number
   // of elements that are available.  ie, if we have 3 elements,
   // we pick the middle one of those as the median.
   // Also, because the sort function sorts the data from low to high,
   // we have to calculate the index from the high end of the array.
   median = sorted_data[MEDIAN_FILTER_WIDTH - 1 - num_elements/2];

   return(median);
}


/*
//-------------------------------------------------------------
// This function calculates the Mean (average).

int16 mean_filter(int16 latest_element)
{
static int16 input_buffer[MEAN_FILTER_WIDTH];
static char inbuf_index = 0;
static char num_elements = 0;
int32 mean;
int32 sum;
char i;

// Insert incoming data element into circular input buffer.
input_buffer[inbuf_index] = latest_element;
inbuf_index++;
if(inbuf_index >= MEAN_FILTER_WIDTH)  // If index went past buffer end
   inbuf_index = 0;       // then reset it to start of buffer

if(num_elements < MEAN_FILTER_WIDTH)
   num_elements++;

// Calculate the mean.  This is done by summing up the
// values and dividing by the number of elements.
sum = 0;
for(i = 0; i < num_elements; i++)
    sum += input_buffer[i];

// Round-off the result by adding half the divisor to
// the numerator.
mean = (sum + (int32)(num_elements >> 1)) / num_elements;

return((int16)mean);
}

*/
