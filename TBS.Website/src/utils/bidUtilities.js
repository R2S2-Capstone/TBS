const bidUtilities = {
  parseBidStatus: (status) => {
    if (status === 0) {
      return 'Open' 
    } else if (status === 1) {
      return 'Approved'
    } else if (status === 2) {
      return 'Declined'
    } else if (status === 3) {
      return 'Cancelled'
    } else if (status === 4) {
      return 'Pending Payment'
    } else if (status === 5) {
      return 'Payment Recieved'
    } else {
      return 'Unknown'
    }
  },
}
export default bidUtilities