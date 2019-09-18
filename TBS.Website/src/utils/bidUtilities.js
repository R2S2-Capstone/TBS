const bidUtilities = {
  parseBidStatus: (status) => {
    if (status === 0) {
      return 'Open' 
    } else if (status === 1) {
      return 'Declined'
    } else if (status === 2) {
      return 'Cancelled'
    } else if (status === 3) {
      return 'Pending Delivery'
    } else if (status === 4) {
      return 'Pending Delivery Approval'
    } else if (status === 5) {
      return 'Completed'
    } else {
      return 'Unknown'
    }
  },
}
export default bidUtilities