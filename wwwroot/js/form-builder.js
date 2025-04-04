/**
 * Helper functions for the form builder
 */

// API calls to manage forms
const formApi = {
    // Get all forms
    getForms: async () => {
        try {
            const response = await fetch(formatApiUrl('forms'));
            if (!response.ok) {
                throw new Error('Failed to fetch forms');
            }
            return await response.json();
        } catch (error) {
            console.error('Error fetching forms:', error);
            throw error;
        }
    },

    // Get a specific form by ID
    getForm: async (id) => {
        try {
            const response = await fetch(formatApiUrl(`forms/${id}`));
            if (!response.ok) {
                throw new Error(`Failed to fetch form with ID: ${id}`);
            }
            return await response.json();
        } catch (error) {
            console.error(`Error fetching form ${id}:`, error);
            throw error;
        }
    },

    // Create a new form
    createForm: async (formData) => {
        try {
            const response = await fetch(formatApiUrl('forms'), {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formData)
            });
            if (!response.ok) {
                throw new Error('Failed to create form');
            }
            return await response.json();
        } catch (error) {
            console.error('Error creating form:', error);
            throw error;
        }
    },

    // Update an existing form
    updateForm: async (id, formData) => {
        try {
            const response = await fetch(formatApiUrl(`forms/${id}`), {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formData)
            });
            if (!response.ok) {
                throw new Error(`Failed to update form with ID: ${id}`);
            }
            return response.status === 204 ? true : await response.json();
        } catch (error) {
            console.error(`Error updating form ${id}:`, error);
            throw error;
        }
    },

    // Delete a form
    deleteForm: async (id) => {
        try {
            const response = await fetch(formatApiUrl(`forms/${id}`), {
                method: 'DELETE'
            });
            if (!response.ok) {
                throw new Error(`Failed to delete form with ID: ${id}`);
            }
            return response.status === 204;
        } catch (error) {
            console.error(`Error deleting form ${id}:`, error);
            throw error;
        }
    }
};

// Submission API calls
const submissionApi = {
    // Get all submissions for a form
    getFormSubmissions: async (formId) => {
        try {
            const response = await fetch(formatApiUrl(`submissions/form/${formId}`));
            if (!response.ok) {
                throw new Error(`Failed to fetch submissions for form: ${formId}`);
            }
            return await response.json();
        } catch (error) {
            console.error(`Error fetching submissions for form ${formId}:`, error);
            throw error;
        }
    },

    // Get a specific submission
    getSubmission: async (id) => {
        try {
            const response = await fetch(formatApiUrl(`submissions/${id}`));
            if (!response.ok) {
                throw new Error(`Failed to fetch submission with ID: ${id}`);
            }
            return await response.json();
        } catch (error) {
            console.error(`Error fetching submission ${id}:`, error);
            throw error;
        }
    },

    // Create a new submission
    createSubmission: async (submissionData) => {
        try {
            const response = await fetch(formatApiUrl('submissions'), {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(submissionData)
            });
            if (!response.ok) {
                throw new Error('Failed to create submission');
            }
            return await response.json();
        } catch (error) {
            console.error('Error creating submission:', error);
            throw error;
        }
    },

    // Delete a submission
    deleteSubmission: async (id) => {
        try {
            const response = await fetch(formatApiUrl(`submissions/${id}`), {
                method: 'DELETE'
            });
            if (!response.ok) {
                throw new Error(`Failed to delete submission with ID: ${id}`);
            }
            return response.status === 204;
        } catch (error) {
            console.error(`Error deleting submission ${id}:`, error);
            throw error;
        }
    }
};

// Form templates
const formTemplates = {
    // Feedback form template
    feedback: {
        title: 'Customer Feedback',
        description: 'Help us improve our products and services by sharing your feedback.',
        elements: [
            {
                id: 'name',
                type: 'text',
                label: 'Your Name',
                placeholder: 'Enter your full name',
                helpText: '',
                required: true
            },
            {
                id: 'email',
                type: 'text',
                label: 'Email Address',
                placeholder: 'Enter your email address',
                helpText: 'We will only use your email if you request a response.',
                required: true
            },
            {
                id: 'satisfaction',
                type: 'radio',
                label: 'How satisfied are you with our service?',
                helpText: '',
                required: true,
                options: [
                    { label: 'Very Satisfied', value: '5' },
                    { label: 'Satisfied', value: '4' },
                    { label: 'Neutral', value: '3' },
                    { label: 'Dissatisfied', value: '2' },
                    { label: 'Very Dissatisfied', value: '1' }
                ]
            },
            {
                id: 'improvements',
                type: 'textarea',
                label: 'What can we do to improve our service?',
                placeholder: 'Your suggestions for improvement',
                helpText: '',
                required: false
            },
            {
                id: 'recommendLikelihood',
                type: 'select',
                label: 'How likely are you to recommend us to others?',
                placeholder: 'Select an option',
                helpText: '',
                required: true,
                options: [
                    { label: 'Very Likely', value: '5' },
                    { label: 'Likely', value: '4' },
                    { label: 'Neutral', value: '3' },
                    { label: 'Unlikely', value: '2' },
                    { label: 'Very Unlikely', value: '1' }
                ]
            }
        ]
    },
    
    // Survey template
    survey: {
        title: 'General Survey',
        description: 'Please complete this survey to help us understand your preferences and needs.',
        elements: [
            {
                id: 'fullName',
                type: 'text',
                label: 'Full Name',
                placeholder: 'Enter your full name',
                helpText: '',
                required: true
            },
            {
                id: 'age',
                type: 'select',
                label: 'Age Group',
                placeholder: 'Select your age group',
                helpText: '',
                required: true,
                options: [
                    { label: 'Under 18', value: 'under_18' },
                    { label: '18-24', value: '18_24' },
                    { label: '25-34', value: '25_34' },
                    { label: '35-44', value: '35_44' },
                    { label: '45-54', value: '45_54' },
                    { label: '55-64', value: '55_64' },
                    { label: '65 or older', value: '65_plus' }
                ]
            },
            {
                id: 'interests',
                type: 'checkbox',
                label: 'What are your interests?',
                helpText: 'Select all that apply',
                required: false,
                options: [
                    { label: 'Technology', value: 'technology' },
                    { label: 'Sports', value: 'sports' },
                    { label: 'Arts & Culture', value: 'arts' },
                    { label: 'Travel', value: 'travel' },
                    { label: 'Food & Cooking', value: 'food' },
                    { label: 'Health & Fitness', value: 'health' },
                    { label: 'Other', value: 'other' }
                ]
            },
            {
                id: 'feedback',
                type: 'textarea',
                label: 'Additional Comments',
                placeholder: 'Please share any additional thoughts or feedback',
                helpText: '',
                required: false
            }
        ]
    },
    
    // Registration form template
    registration: {
        title: 'Event Registration',
        description: 'Register for our upcoming event.',
        elements: [
            {
                id: 'firstName',
                type: 'text',
                label: 'First Name',
                placeholder: 'Enter your first name',
                helpText: '',
                required: true
            },
            {
                id: 'lastName',
                type: 'text',
                label: 'Last Name',
                placeholder: 'Enter your last name',
                helpText: '',
                required: true
            },
            {
                id: 'email',
                type: 'text',
                label: 'Email Address',
                placeholder: 'Enter your email address',
                helpText: 'We will send confirmation details to this email.',
                required: true
            },
            {
                id: 'phone',
                type: 'text',
                label: 'Phone Number',
                placeholder: 'Enter your phone number',
                helpText: '',
                required: false
            },
            {
                id: 'eventDate',
                type: 'date',
                label: 'Event Date',
                helpText: 'Select which date you would like to attend',
                required: true
            },
            {
                id: 'dietaryRestrictions',
                type: 'textarea',
                label: 'Dietary Restrictions',
                placeholder: 'Please list any dietary restrictions or allergies',
                helpText: '',
                required: false
            },
            {
                id: 'termsAgreement',
                type: 'checkbox',
                label: 'Terms and Conditions',
                helpText: '',
                required: true,
                options: [
                    { label: 'I agree to the terms and conditions', value: 'agree' }
                ]
            }
        ]
    },
    
    // Request form template
    request: {
        title: 'Service Request',
        description: 'Submit a request for our services.',
        elements: [
            {
                id: 'requestorName',
                type: 'text',
                label: 'Your Name',
                placeholder: 'Enter your full name',
                helpText: '',
                required: true
            },
            {
                id: 'department',
                type: 'text',
                label: 'Department',
                placeholder: 'Enter your department',
                helpText: '',
                required: true
            },
            {
                id: 'requestType',
                type: 'select',
                label: 'Request Type',
                placeholder: 'Select request type',
                helpText: '',
                required: true,
                options: [
                    { label: 'IT Support', value: 'it_support' },
                    { label: 'Maintenance', value: 'maintenance' },
                    { label: 'Administrative', value: 'administrative' },
                    { label: 'Human Resources', value: 'hr' },
                    { label: 'Other', value: 'other' }
                ]
            },
            {
                id: 'priority',
                type: 'radio',
                label: 'Priority Level',
                helpText: '',
                required: true,
                options: [
                    { label: 'Low', value: 'low' },
                    { label: 'Medium', value: 'medium' },
                    { label: 'High', value: 'high' },
                    { label: 'Urgent', value: 'urgent' }
                ]
            },
            {
                id: 'description',
                type: 'textarea',
                label: 'Request Description',
                placeholder: 'Please provide details about your request',
                helpText: 'Be as specific as possible',
                required: true
            },
            {
                id: 'attachments',
                type: 'file',
                label: 'Attachments',
                helpText: 'Upload any relevant documents or images',
                required: false,
                acceptedFiles: 'PDF, DOC, DOCX, JPG, PNG up to 10MB'
            }
        ]
    }
};

// Generate a unique ID for form elements
const generateUniqueId = () => {
    return 'element-' + Date.now() + '-' + Math.random().toString(36).substr(2, 9);
};

// Load a form template
const loadFormTemplate = (templateName) => {
    const template = formTemplates[templateName];
    if (!template) {
        console.error(`Template '${templateName}' not found`);
        return null;
    }
    
    // Ensure all elements have unique IDs
    template.elements.forEach(element => {
        if (!element.id) {
            element.id = generateUniqueId();
        }
    });
    
    return template;
};

// Initialize the form builder page
document.addEventListener('DOMContentLoaded', () => {
    // Check if we're on the form builder page
    const formBuilderElement = document.getElementById('form-builder-container');
    if (!formBuilderElement) return;
    
    // Check URL for template parameter
    const urlParams = new URLSearchParams(window.location.search);
    const templateName = urlParams.get('template');
    
    if (templateName && formTemplates[templateName]) {
        // Load the template
        const template = loadFormTemplate(templateName);
        if (template && window.formBuilder) {
            // Set form title and description
            window.formBuilder.formTitle = template.title;
            window.formBuilder.formDescription = template.description;
            
            // Add template elements
            template.elements.forEach(element => {
                window.formBuilder.formElements.push(element);
            });
            
            // Show notification
            showNotification(`Template "${template.title}" loaded successfully`, 'success');
        }
    }
    
    // Check if we need to load an existing form for editing
    const formId = formBuilderElement.dataset.formId;
    if (formId) {
        formApi.getForm(formId)
            .then(form => {
                if (form && window.formBuilder) {
                    // Set form data
                    window.formBuilder.formId = form.id;
                    window.formBuilder.formTitle = form.title;
                    window.formBuilder.formDescription = form.description;
                    window.formBuilder.formElements = form.elements;
                    window.formBuilder.formStatus = form.status;
                    
                    // Show notification
                    showNotification(`Form "${form.title}" loaded for editing`, 'success');
                }
            })
            .catch(error => {
                console.error('Error loading form:', error);
                showNotification('Error loading form. Please try again.', 'error');
            });
    }
});