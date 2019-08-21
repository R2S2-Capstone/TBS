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
        } else {
        return 'Unknown'
        }
    },
}
export default bidUtilities